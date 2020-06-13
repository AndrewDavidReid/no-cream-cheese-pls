$configuration = $args[0]

if ([string]::IsNullOrEmpty($configuration)) {
  $configuration = "debug"
}

$db_host = "localhost"
$db_port = 5432
$db_name = "nccp-local"
$db_username = "postgres"
$db_pass = "postgres"

$connection_str = [string]::Format("Host={0};Port={1};Database={2};Uid={3};Pwd={4};", $db_host, $db_port, $db_name, $db_username, $db_pass)

# Params
dotnet run -c $configuration -s $connection_str
