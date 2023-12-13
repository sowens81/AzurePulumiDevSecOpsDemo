using Pulumi;
using Pulumi.AzureNative.Resources;

namespace WeatherAPI.Pulumi.Azure.Components
{
    public class ResourceGroupComponent : ComponentResource
    {

        public Output<string> Name { get; private set; }
        public Output<string> Id { get; private set; }
        public Output<string> Location { get; private set; }

        public ResourceGroupComponent(string name, ResourceGroupComponentArgs args, ComponentResourceOptions? opts = null)
            : base("Component:Resource:ResourceGroup", name, opts)
        {

            // Validate input arguments
            if (string.IsNullOrEmpty(args.Location.ToString()))
            {
                throw new ArgumentException("Location must be provided for the ResourceGroupComponent.");
            }

            // Extending Tags with a new Key:Value of ResourceName:Value

            InputMap<string> resourceNameTag = new()
            {
                {"ResourceName", name }
            };

            InputMap<string> resourceGroupTags = InputMap<string>.Merge(args.Tags, resourceNameTag);

            // Creating the ResourceGroup
            ResourceGroup resourceGroup = new(name, new ResourceGroupArgs
            {
                ResourceGroupName = name,
                Location = args.Location,
                Tags = resourceGroupTags
            }, new CustomResourceOptions { Parent = this }); ;

            // Initializing output properties
            this.Id = resourceGroup.Id;
            this.Name = resourceGroup.Name;
            this.Location = resourceGroup.Location;

            // Registering outputs
            this.RegisterOutputs(new Dictionary<string, object?>
            {
                { "Id", resourceGroup.Id },
                { "Name", resourceGroup.Name },
                { "Location", resourceGroup.Location }
            });
        }
    }

    public class ResourceGroupComponentArgs
    {
        public Input<string> Location { get; set; }
        public InputMap<string> Tags { get; set; }
    }
}