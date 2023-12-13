using System;
using WeatherAPI.Pulumi.Azure.Components;
using Pulumi;
using Pulumi.AzureNative.Resources;

namespace WeatherAPI.IaC
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

