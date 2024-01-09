using System.Reflection;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.WebAssembly.Services;

namespace LazyTest.Client
{
    public partial class App
    {
        [Inject]
        private LazyAssemblyLoader AssemblyLoader { get; set; } = null!;

        private async Task OnNavigateAsync(NavigationContext args)
        {
            if (args.Path == "lazy")
            {
                var assemblies = await AssemblyLoader.LoadAssembliesAsync(new[] { "LazyTest.Client.Lazy.wasm" });
                lazyLoadedAssemblies.AddRange(assemblies);
                Console.WriteLine($"loaded assemblies: {string.Join(",", assemblies.Select(i => i.FullName))}");
            }
        }

        private List<Assembly> lazyLoadedAssemblies = new();
    }
}
