using System.Collections.Immutable;
using System.Threading.Tasks;
using Pulumi;
using Pulumi.Testing;

namespace WeatherAPI.IaC.UnitTests
{
    /// <summary>
    /// x
    /// </summary>
    class Mocks : IMocks
    {
        /// <summary>
        /// x
        /// </summary>
        public Task<(string? id, object state)> NewResourceAsync(MockResourceArgs args)
        {
            var outputs = ImmutableDictionary.CreateBuilder<string, object>();

            // Forward all input parameters as resource outputs, so that we could test them.
            outputs.AddRange(args.Inputs);

            // Default the resource ID to `{name}_id` if null.
            args.Id ??= $"{args.Name}_id";

            return Task.FromResult<(string? id, object state)>((args.Id, (object)outputs));
        }

        /// <summary>
        /// x
        /// </summary>
        public Task<object> CallAsync(MockCallArgs args)
        {
            var outputs = ImmutableDictionary.CreateBuilder<string, object>();

            if (args.Token == "aws:index/getAmi:getAmi")
            {
                outputs.Add("architecture", "x86_64");
                outputs.Add("id", "ami-0eb1f3cdeeb8eed2a");
            }

            return Task.FromResult((object)outputs);
        }
    }

    /// <summary>
    /// x
    /// </summary>
    public static class Testing
    {
        /// <summary>
        /// x
        /// </summary>
        public static Task<ImmutableArray<Resource>> RunAsync<T>() where T : Stack, new()
        {
            return Deployment.TestAsync<T>(new Mocks(), new TestOptions { IsPreview = false });
        }

        /// <summary>
        /// x
        /// </summary>
        public static Task<T> GetValueAsync<T>(this Output<T> output)
        {
            var tcs = new TaskCompletionSource<T>();
            output.Apply(v =>
            {
                tcs.SetResult(v);
                return v;
            });
            return tcs.Task;
        }
    }
}