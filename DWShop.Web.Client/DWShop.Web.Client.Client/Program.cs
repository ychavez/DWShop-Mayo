using Blazored.LocalStorage;
using DWShop.Web.Infrastructure.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

namespace DWShop.Web.Client.Client
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.Services.AddScoped<ClientPreferencesServices>();
            builder.Services.AddBlazoredLocalStorage();

            builder.Services.AddMudServices();

            await builder.Build().RunAsync();
        }
    }
}
