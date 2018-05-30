#load "utils.cake"

var target = Argument("target", "Default");
var appName = Argument("appName", "no-cream-cheese-pls");
var configuration = Argument("configuration", "Release");
var deploymentEnvironment = Argument("deploymentEnvironment", "Development");
var clientProjectDirectory = Directory("./client");
var serverProjectDirectory = Directory("./server");
var helmChartDirectory = Directory("./helm-chart");
var dockerRegistry = ArgumentEnvironmentOrDefault("dockerRegistry", "DOCKER_REGISTRY", "");
var branchName = Argument("branchName", "dev");
var commitSha = Argument("commitSha", "testing");
var shortenedCommitSha = ShortenedCommitSha(commitSha);
var webDockerImageTag = BuildDockerImageTag(dockerRegistry, appName, branchName, shortenedCommitSha);
var migrationsDockerImageTag = BuildDockerImageTag(dockerRegistry, $"{appName}-db-migrations", branchName, shortenedCommitSha);

// All things database
var integrationTestingDbHost = Argument("integrationTestingDbHost", "");
var integrationTestingDbPort = Argument("integrationTestingDbPort", "");
var integrationTestingDbUser = Argument("integrationTestingDbUser", "");
var integrationTestingDbPassword = Argument("integrationTestingDbPassword", "");
var integrationTestingDbName = Argument("integrationTestingDbName", "");


Task("Restore Client Packages")
.Does(() => {

  var exitCode = StartProcess("npm", new ProcessSettings {
    Arguments = "install",
    WorkingDirectory = clientProjectDirectory
  });
});

Task("Build Client Project")
.IsDependentOn("Restore Client Packages")
.Does(() => {

  var exitCode = StartProcess("npm", new ProcessSettings {
    Arguments = $"run build --{deploymentEnvironment.ToLower()}",
    WorkingDirectory = clientProjectDirectory
  });
});

Task("Build Web Docker Image")
.IsDependentOn("Build Client Project")
.Does(() => {

  var exitCode = StartProcess("docker", new ProcessSettings {
    Arguments = $"build  -f ./NoCreamCheesePls/Dockerfile --tag {webDockerImageTag} .",
    WorkingDirectory = serverProjectDirectory
  });
});

Task("Build Migrations Docker Image")
.IsDependentOn("Build Web Docker Image")
.Does(() => {

  var exitCode = StartProcess("docker", new ProcessSettings {
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

  var exitCode = StartProcess("docker", new ProcessSettings {
    Arguments = $"push {webDockerImageTag}"
  });
});

Task("Push Migrations Docker Image to Registry")
.IsDependentOn("Push Web Docker Image to Registry")
.Does(() => {

  var exitCode = StartProcess("docker", new ProcessSettings {
    Arguments = $"push {migrationsDockerImageTag}"
  });
});

Task("Deploy to Kubernetes Cluster")
.IsDependentOn("Push Migrations Docker Image to Registry")
.Does(() => {

  var exitCode = StartProcess("helm", new ProcessSettings {
    Arguments = $@"upgrade
    --debug
    --install
    --wait
    --namespace {deploymentEnvironment.ToLower()}
    --set branchName={branchName}
    --set webDockerImage={webDockerImageTag}
    --set dbMigrationsDockerImage={migrationsDockerImageTag}
    {appName} .",
    WorkingDirectory = helmChartDirectory
  });
});

Task("Default")
.IsDependentOn("Deploy to Kubernetes Cluster");

RunTarget(target);
