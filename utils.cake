Func<string, string> RequiredArgument = (argumentName) => {
  return Argument(argumentName, "") ?? throw new ArgumentException($"Required argument: {argumentName}");
};

Func<string, string, string, string> ArgumentEnvironmentOrDefault = (argumentName, environmentVariableName, defaultValue) => {
  var argumentValue = Argument(argumentName, "");

  if (!string.IsNullOrEmpty(argumentValue))
  {
    return argumentValue;
  }

  var environmentVariableValue = EnvironmentVariable(environmentVariableName);

  if (!string.IsNullOrEmpty(environmentVariableValue))
  {
    return environmentVariableValue;
  }

  return defaultValue;
};

Func<string, string> ShortenedCommitSha = (commitSha) => {
  return commitSha.Substring(0, Math.Min(commitSha.Length, 10));
};
Func<string, string, string, string, string> BuildDockerImageTag = (registry, imageName, tightBranchName, shortenedCommitSha) => {
  return $"{registry}/{imageName}:{tightBranchName}-{shortenedCommitSha}";
};

Func<string, string, string, string> BuildDeploymentUrl = (domain, deploymentEnvironment, branchName) => {
  if (deploymentEnvironment == "Production")
  {
    return domain;
  }

  // TODO: Sanitize the branch name
  return $"{branchName}.{domain}";
};

Action<int> ThrowOnNonZeroExitCode = (exitCode) => {
  if (exitCode != 0)
  {
    throw new Exception("Non 0 exit code returned");
  }
};
