# Define variables
$environment = @("dev", "prod")
$location = ""
$locationShort = ""
$costCenter = ""
$tentantId = ""

# Loop through each environment
foreach ($env in $environment) {
    # Construct the resource group name
    $resourceGroupName = "so-rg-core-" + $locationShort + "-" + $env + "-01"
    $keyVaultName = "sokvcoreplt" + $locationShort + $env +"01"

    # Execute Pulumi config set command
    pulumi stack init $env
    pulumi stack select $env
    pulumi config set EPAMWeatherAPI:tenantId $tentantId 
    pulumi config set EPAMWeatherAPI:environment $env
    pulumi config set EPAMWeatherAPI:environmentShort $env
    pulumi config set EPAMWeatherAPI:location $location
    pulumi config set EPAMWeatherAPI:costCenter $costCenter
    pulumi config set EPAMWeatherAPI:resourceGroupName $resourceGroupName
    pulumi config set EPAMWeatherAPI:keyVaultName $keyVaultName
}