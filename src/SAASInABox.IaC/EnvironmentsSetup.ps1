# Define variables
$environments = @("dev", "prod")
$location = "UkSouth"
$locationShort = "uks"
$costCenter = "123456"

# Read Azure Tenant Id
$tenantId = Read-Host "Input your Azure Tenant Id"

# Loop through each environment
foreach ($env in $environments) {
    # Construct the resource group name
    $resourceGroupName = "so-rg-core-" + $locationShort + "-" + $env + "-01"
    $keyVaultName = "sokvcoreplt" + $locationShort + $env +"01"

    # Execute Pulumi config set command
    pulumi stack init $env
    pulumi stack select $env
    pulumi config set EPAMSAASInABox:tenantId $tenantId
    pulumi config set EPAMSAASInABox:environment $env
    pulumi config set EPAMSAASInABox:location $location
    pulumi config set EPAMSAASInABox:locationShort $locationShort
    pulumi config set EPAMSAASInABox:costCenter $costCenter
    pulumi config set EPAMSAASInABox:resourceGroupName $resourceGroupName
    pulumi config set EPAMSAASInABox:keyVaultName $keyVaultName
}
