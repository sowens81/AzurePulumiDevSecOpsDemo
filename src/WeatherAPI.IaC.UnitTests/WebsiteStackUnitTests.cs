using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Pulumi;
using Pulumi.Azure.Core;
using Pulumi.Testing;

namespace WeatherAPI.IaC.UnitTests
{
    /// <summary>
    /// x
    /// </summary>
    [TestFixture]
    public class WebsiteStackUnitTests
	{

        /// <summary>
        /// x
        /// </summary>
        [Test]
        public async Task Assert_Resource_Group_Name_And_Exists()
        {
            // Arrange

            // Act
            var resources = await Testing.RunAsync<WebsiteStack>();
            var resourceGroup = resources.OfType<ResourceGroup>().FirstOrDefault();

            //
            resourceGroup.Should().NotBeNull("Resource Group Instance not found");
            resourceGroup.Name.Should().Be("www-prod-rg", "Resource Group Name incorrect!");

        }
    }
}

