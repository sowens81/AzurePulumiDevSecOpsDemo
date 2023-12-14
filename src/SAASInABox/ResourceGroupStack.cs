using System;
using SAASInABox.IaC.Components;
using Pulumi;
using Pulumi.AzureNative.Resources;

namespace SAASInABox
{
	public class ResourceGroupStack : Stack
	{
		public ResourceGroupStack(string ResourceGroupName, string Location)
		{
            InputMap<string> tags = new()
            {
                { "resourceName", ResourceGroupName }
            };


            ResourceGroup resourceGroup = new(ResourceGroupName, new ResourceGroupArgs()
			{
				Location = Location,
				Tags = tags
			});
		}
	}
}

