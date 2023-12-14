using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pulumi;
using Pulumi.Automation;

namespace SAASInABox.Controllers;

[ApiController]
[Route("[controller]")]
public class PulumiController : ControllerBase
{
    private readonly ILogger<PulumiController> _logger;

    private string? ProjectName {get; set;}
    private string? StackName {get; set;}
    private bool Destroy {get; set;}


    public PulumiController(ILogger<PulumiController> logger)
    {
        _logger = logger;
    }

    [HttpPost(Name = "DeployResource")]
    public IEnumerable<string> Post(ResourceConfiguration cfg, string destroy = "false")
    {
        if(destroy == "true")
        {
            Destroy = true;
        }

        ResourceGroupStack resourceGroupStack = new(cfg.ResourceGroupName, cfg.Location);

        var program = PulumiFn.Create<resourceGroupStack>();

        ProjectName = cfg.ProjectName;
        StackName = cfg.StackName;

        // create or select a stack matching the specified name and project
        // this will set up a workspace with everything necessary to run our inline program (program)
        var stackArgs = new InlineProgramArgs(projectName, stackName, program);
        var stack = await LocalWorkspace.CreateOrSelectStackAsync(stackArgs);
        Console.WriteLine("successfully initialized stack");

        // for inline programs, we must manage plugins ourselves
        Console.WriteLine("installing plugins...");
        await stack.Workspace.InstallPluginAsync("azure-native", "v2.21.2");
        Console.WriteLine("plugins installed");

        // set stack configuration specifying the region to deploy
        Console.WriteLine("setting up config...");
        await stack.SetConfigAsync("azure-native:location", new ConfigValue(cfg.Location));
        await stack.SetConfigAsync($"{ProjectName}:location", new ConfigValue(cfg.Location));
        await stack.SetConfigAsync($"{ProjectName}:resourcegroupname", new ConfigValue(cfg.Location));
        Console.WriteLine("config set");

        Console.WriteLine("refreshing stack...");
        await stack.RefreshAsync(new RefreshOptions { OnStandardOutput = Console.WriteLine });
        Console.WriteLine("refresh complete");
        if(Destroy)
        {
            Console.WriteLine("destroying stack...");
            await stack.DestroyAsync(new DestroyOptions { OnStandardOutput = Console.WriteLine });
            Console.WriteLine("stack destroy complete");

            return "stack destroy complete"
        }
        else
        {
            Console.WriteLine("updating stack...");
            var result = await stack.UpAsync(new UpOptions { OnStandardOutput = Console.WriteLine });

            if (result.Summary.ResourceChanges != null)
            {
                Console.WriteLine("update summary:");
                foreach (var change in result.Summary.ResourceChanges)
                    Console.WriteLine($"    {change.Key}: {change.Value}");
            }

            Console.WriteLine($"website url: {result.Outputs["website_url"].Value}");
             return $"website url: {result.Outputs["website_url"].Value}";
        }
    }
}
