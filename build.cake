#load "utils.cake"

// Required args
var dbName = RequiredArgument("dbName");
var dbHost = RequiredArgument("dbHost");
var branchName = RequiredArgument("branchName");
var commitSha = RequiredArgument("commitSha");
var appName = RequiredArgument("appName");
var deploymentEnvironment = RequiredArgument("deploymentEnvironment");
var domain = RequiredArgument("domain");
var dockerRegistry = RequiredArgument("dockerRegistry");

// Rarely overrided args.
var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");
var databaseProvider = Argument("databaseProvider", "postgres");
var dbPort = Argument("dbPort", "5432`");

// vars
var clientProjectDirectory = Directory("./client");
var serverProjectDirectory = Directory("./server");
var helmChartDirectory = Directory("./helm-chart");
var shortenedCommitSha = ShortenedCommitSha(commitSha);
var webDockerImageTag = BuildDockerImageTag(dockerRegistry, appName, branchName, shortenedCommitSha);
var migrationsDockerImageTag = BuildDockerImageTag(dockerRegistry, $"{appName}-db-migrations", branchName, shortenedCommitSha);
var integrationTestsDockerImageTag = BuildDockerImageTag(dockerRegistry, $"{appName}-integration-tests", branchName, shortenedCommitSha);
var deploymentUrl = BuildDeploymentUrl(domain, deploymentEnvironment, branchName);

// Integration testing connection string details
var integrationTestingDbName = "test-db";
var integrationTestingDbPassword = "password";
var integrationTestingDatabaseContainerName = "nccp-testing-postgres";
var integrationTestingNetworkName = "integration-testing-network";


Task("Restore Client Packages")
.Does(() => {

  StartProcess("npm", new ProcessSettings {
    Arguments = "install",
    WorkingDirectory = clientProjectDirectory
  });
});

Task("Build Client Project")
.IsDependentOn("Restore Client Packages")
.Does(() => {

  StartProcess("npm", new ProcessSettings {
    Arguments = $"run build --{deploymentEnvironment.ToLower()}",
    WorkingDirectory = clientProjectDirectory
  });
});

Task("Build Web Docker Image")
// .IsDependentOn("Build Client Project")
.Does(() => {

  StartProcess("docker", new ProcessSettings {
    Arguments = $"build -f ./NoCreamCheesePls/Dockerfile --tag {webDockerImageTag} .",
    WorkingDirectory = serverProjectDirectory
  });
});

Task("Build Migrations Docker Image")
.IsDependentOn("Build Web Docker Image")
.Does(() => {

  StartProcess("docker", new ProcessSettings {
    Arguments = $@"build
    -f ./NoCreamCheesePls.Data.Migrations/Dockerfile
    --tag {migrationsDockerImageTag}
    --build-arg MIGRATIONS_PROJECT=NoCreamCheesePls.Data.Migrations .",
    WorkingDirectory = serverProjectDirectory
  });
});

Task("Run Integration Tests")
.IsDependentOn("Build Migrations Docker Image")
.Does(async () => {

  Information($"Creating integration test container network: {integrationTestingNetworkName}");
  StartProcess("docker", new ProcessSettings {
    Arguments = $"network create {integrationTestingNetworkName}"
  });

  Information($"Creating database container for integration tests: {integrationTestingDatabaseContainerName}");
  // Spin up a test database
  StartProcess("docker", new ProcessSettings {
    Arguments = $@"run
    --name {integrationTestingDatabaseContainerName}
    --network={integrationTestingNetworkName}
    -e POSTGRES_USER=postgres
    -e POSTGRES_DB={integrationTestingDbName}
    -e POSTGRES_PASSWORD={integrationTestingDbPassword}
    -d postgres:9.6.9"
  });

  try
  {
    // It takes a bit of time for the postgres container to come online.
    await System.Threading.Tasks.Task.Delay(7500);

    Information($"Running migrations against integration test database: {integrationTestingDatabaseContainerName}");
    var migrationsExitCode = StartProcess("docker", new ProcessSettings {
        Arguments = $@"run
        --rm
        --network={integrationTestingNetworkName}
        -e DEPLOYMENT_ENVIRONMENT={deploymentEnvironment}
        -e DATABASE_PROVIDER={databaseProvider}
        -e CONNECTION_STRING=""Host={integrationTestingDatabaseContainerName};Port=5432;Database={integrationTestingDbName};Username=postgres;Password={integrationTestingDbPassword};""
        {migrationsDockerImageTag}"
      });

    ThrowOnNonZeroExitCode(migrationsExitCode);

    Information($"Building integration test container image: {integrationTestsDockerImageTag}");
    var integrationTestsBuildExitCode = StartProcess("docker", new ProcessSettings {
      Arguments = $@"build --tag {integrationTestsDockerImageTag} --build-arg TESTS_PROJECT='NoCreamCheesePls.IntegrationTests' .",
      WorkingDirectory = serverProjectDirectory
    });

    ThrowOnNonZeroExitCode(integrationTestsBuildExitCode);

    Information($"Running integration tests");
    // Always use the development environment for integration tests.
    var integrationTestsExitCode = StartProcess("docker", new ProcessSettings {
      Arguments = $@"run
      --rm
      --network={integrationTestingNetworkName}
      -e ASPNETCORE_ENVIRONMENT=Development
      -e NCCP_DB_HOST=${integrationTestingDatabaseContainerName}
      -e NCCP_DB_PORT=5432
      -e NCCP_DB_NAME=${integrationTestingDbName}
      -e NCCP_DB_USER=postgres
      -e NCCP_DB_PASSWORD=${integrationTestingDbPassword}
      {integrationTestsDockerImageTag}"
    });

    ThrowOnNonZeroExitCode(integrationTestsExitCode);
  }
  finally
  {
    Information($"Deleting integration test database container: {integrationTestingDatabaseContainerName}");
    StartProcess("docker", new ProcessSettings {
      Arguments = $@"rm --force {integrationTestingDatabaseContainerName}"
    });

    Information($"Deleting integration test container network: {integrationTestingDatabaseContainerName}");
    StartProcess("docker", new ProcessSettings {
      Arguments = $@"network rm {integrationTestingNetworkName}"
    });
  }
});

Task("Push Web Docker Image to Registry")
.IsDependentOn("Run Integration Tests")
.Does(() => {

  StartProcess("docker", new ProcessSettings {
    Arguments = $"push {webDockerImageTag}"
  });
});

Task("Push Migrations Docker Image to Registry")
.IsDependentOn("Push Web Docker Image to Registry")
.Does(() => {

  StartProcess("docker", new ProcessSettings {
    Arguments = $"push {migrationsDockerImageTag}"
  });
});

Task("Deploy to Kubernetes Cluster")
.IsDependentOn("Push Migrations Docker Image to Registry")
.Does(() => {

  StartProcess("helm", new ProcessSettings {
    Arguments = $@"upgrade
    --debug
    --install
    --wait
    --namespace {deploymentEnvironment.ToLower()}
    --set deploymentEnvironment={deploymentEnvironment}
    --set webDockerImage={webDockerImageTag}
    --set dbMigrationsDockerImage={migrationsDockerImageTag}
    --set dbHost={dbHost}
    --set dbName={dbName}
    --set dbPort={dbPort}
    --set deploymentUrl={deploymentUrl}
    --set databaseProvider={databaseProvider}
    {appName}-{deploymentEnvironment.ToLower()} .",
    WorkingDirectory = helmChartDirectory
  });
});

Task("Log Results")
.IsDependentOn("Deploy to Kubernetes Cluster")
.Does(() => {
  Information($"App has successfully been deployed to http://{deploymentUrl}");
});

Task("Default")
.IsDependentOn("Log Results");

RunTarget(target);
