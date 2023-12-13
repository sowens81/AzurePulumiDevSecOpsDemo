using Pulumi;
using KeyVault = Pulumi.AzureNative.KeyVault;

namespace WeatherAPI.Pulumi.Azure.Components
{
    public class KeyVaultComponent : ComponentResource
    {
        public Output<string> Name { get; private set; }
        public Output<string> Id { get; private set; }
        public Output<string> VaultUri { get; private set; }

        public KeyVaultComponent(string name, KeyVaultComponentArgs args, ComponentResourceOptions? opts = null)
            : base("Component:KeyVault:Vault", name, opts)
        {
            // Validate input arguments
            if (string.IsNullOrEmpty(args.ResourceGroupName.ToString()))
            {
                throw new ArgumentException("ResourceGroupName must be provided for the KeyVaultComponent.");
            }

            if (string.IsNullOrEmpty(args.TenantId.ToString()))
            {
                throw new ArgumentException("TenantId must be provided for the KeyVaultComponent.");
            }
            // Extending Tags with a new Key:Value of ResourceName:Value

            InputMap<string> resourceNameTag = new()
            {
                {"ResourceName", name }
            };

            InputMap<string> keyVaultTags = InputMap<string>.Merge(args.Tags, resourceNameTag);

            // Creating the Key Vault
            KeyVault.Vault vault = new(name, new()
            {
                VaultName = name,
                ResourceGroupName = args.ResourceGroupName,
                Tags = keyVaultTags,
                Properties = new KeyVault.Inputs.VaultPropertiesArgs
                {
                    EnabledForDeployment = true,
                    EnabledForDiskEncryption = true,
                    EnabledForTemplateDeployment = true,
                    EnableRbacAuthorization = true,
                    EnableSoftDelete = false,
                    PublicNetworkAccess = "Enabled",
                    Sku = new KeyVault.Inputs.SkuArgs
                    {
                        Family = "A",
                        Name = KeyVault.SkuName.Standard,
                    },
                    TenantId = args.TenantId,
                }
            },
            new CustomResourceOptions { Parent = this }
            );

            // Initializing output properties
            Id = vault.Id;
            Name = vault.Name;
            VaultUri = vault.Properties.Apply(properties => properties.VaultUri);

            // Registering outputs
            this.RegisterOutputs(new Dictionary<string, object?>
            {
                { "Id", vault.Id },
                { "Name", vault.Name },
                { "Location", vault.Location },
                { "VaultUri", vault.Properties.Apply(properties => properties.VaultUri) }
            });
        }
    }
    public class KeyVaultComponentArgs
    {
        public Input<string> ResourceGroupName { get; set; }
        public Input<string> TenantId { get; set; }
        public InputMap<string> Tags { get; set; }
    }
}