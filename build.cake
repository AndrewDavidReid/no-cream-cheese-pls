#load "utils.cake"

// Required args
var dbName = RequiredArgument("dbName");
var branchName = RequiredArgument("branchName");
var commitSha = RequiredArgument("commitSha");
var appName = RequiredArgument("appName");
var deploymentEnvironment = RequiredArgument("deploymentEnvironment");
var domain = RequiredArgument("domain");
var dockerRegistry = RequiredArgument("dockerRegistry");

// Rarely overrided args.
var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

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
var integrationTestingDbName = "testing";
var integrationTestingDbPassword = "password";
var integrationTestingDatabaseContainerName = "nccp-testing-db";

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

Task("Run Integration Tests")
.IsDependentOn("Build Client Project")
.Does(() => {

  StartProcess("docker", new ProcessSettings {
    Arguments = $@"run
    --name {integrationTestingDatabaseContainerName}
    -e POSTGRES_DB={integrationTestingDbName}
    -e POSTGRES_PASSWORD={integrationTestingDbPassword}
    -p 5432:5432
    -d postgres:9.6.9"
  });

  try
  {
    StartProcess("docker", new ProcessSettings {
      Arguments = $@"build --tag {integrationTestsDockerImageTag} --build-arg TESTS_PROJECT='NoCreamCheesePls.IntegrationTests' .",
      WorkingDirectory = serverProjectDirectory
    });

    StartProcess("docker", new ProcessSettings {
      Arguments = $@"run
      --rm
      -e ASPNETCORE_ENVIRONMENT='Development'
      -e NCCP_DB_HOST='0.0.0.0'
      -e NCCP_DB_PORT='5432'
      -e NCCP_DB_NAME='${integrationTestingDbName}'
      -e NCCP_DB_USER='postgres'
      -e NCCP_DB_PASSWORD='${integrationTestingDbPassword}'
      {integrationTestsDockerImageTag}",
      WorkingDirectory = serverProjectDirectory
    });
  }
  finally
  {
    StartProcess("docker", new ProcessSettings {
      Arguments = $@"rm --force {integrationTestingDatabaseContainerName}"
    });
  }
});

Task("Build Web Docker Image")
.IsDependentOn("Run Integration Tests")
.Does(() => {

  StartProcess("docker", new ProcessSettings {
    Arguments = $"build  -f ./NoCreamCheesePls/Dockerfile --tag {webDockerImageTag} .",
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

Task("Push Web Docker Image to Registry")
.IsDependentOn("Build Migrations Docker Image")
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
    --set dbName={dbName}
    --set deploymentUrl={deploymentUrl}
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
