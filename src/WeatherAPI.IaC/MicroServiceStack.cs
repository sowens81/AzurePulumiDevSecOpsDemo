using WeatherAPI.Pulumi.Azure.Components;
using Pulumi;
using Pulumi.AzureNative.Resources;

namespace WeatherAPI.IaC
{
    public class MicroServiceStack : Stack
    {
        public MicroServiceStack()
        {
            //Config config = new();

            //// Modifty to load all Configuration as one
            //string tenantId = config.Require("tenantId");
            //string location = config.Require("location");
            //string environment = config.Require("environment");
            //string costCenter = config.Require("costCenter");
            //string resourceGroupName = config.Require("resourceGroupName");
            //string keyVaultName = config.Require("keyVaultName");

            //InputMap<string> tags = new()
            //{
            //    { "Location", location },
            //    { "Environment", environment },
            //    { "CostCenter", costCenter }
            //};

            ResourceGroup resourceGroup = new("www-prod-rg");

            //ResourceGroupComponent resourceGroup = new(resourceGroupName, new ResourceGroupComponentArgs()
            //{
            //    Location = location,
            //    Tags = tags
            //});

            //KeyVaultComponent keyVault = new(keyVaultName, new KeyVaultComponentArgs()
            //{
            //    ResourceGroupName = resourceGroup.Name,
            //    TenantId = tenantId,
            //    Tags = tags
            //});

            //this.ResourceGroupId = resourceGroup.Id;
            //this.ResourceGroupName = resourceGroup.Name;
            //this.KeyVaultId = keyVault.Id;
            //this.KeyVaultName = keyVault.Name;
            //this.KeyVaultUri = keyVault.VaultUri;

        }

        //[Output]
        //public Output<string> ResourceGroupId { get; private set; }

        //[Output]
        //public Output<string> ResourceGroupName { get; private set; }

        //[Output]
        //public Output<string> KeyVaultId { get; private set; }

        //[Output]
        //public Output<string> KeyVaultName { get; private set; }

        //[Output]
        //public Output<string> KeyVaultUri { get; private set; }
    }
}

