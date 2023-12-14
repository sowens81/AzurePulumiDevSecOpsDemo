using System;
using SAASInABox.IaC.Components;
using Pulumi;
using Pulumi.AzureNative.Resources;

namespace SAASInABox.IaC
{
	public class WebsiteStack : Stack
	{
		public WebsiteStack()
		{
			string resourceGroupName = "www-prod-rg";
			string location = "UkSouth";
            InputMap<string> tags = new()
            {
                { "resourceName", "www-prod-rg" }
            };


            ResourceGroupComponent resourceGroup = new(resourceGroupName, new ResourceGroupComponentArgs()
			{
				Location = location,
				Tags = tags
			});
		}
	}
}

