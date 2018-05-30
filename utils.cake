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
Func<string,string,string,string,string> BuildDockerImageTag = (registry, imageName, tightBranchName, shortenedCommitSha) => {
  return $"{registry}/{imageName}:{tightBranchName}-{shortenedCommitSha}";
};
