using Blazored.LocalStorage;
using DWShop.Client.Infrastructure.Constants;
using DWShop.Client.Infrastructure.Extensions;
using DWShop.Web.Client.Components;
using DWShop.Web.Infrastructure.Authentication;
using DWShop.Web.Infrastructure.Services;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor.Services;


namespace DWShop.Web.Client
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveWebAssemblyComponents();

            builder.Services.AddMudServices();

            builder.Services.AddScoped<ClientPreferencesServices>();
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddScoped<DWStateProvider>();
            builder.Services.AddScoped<ILoginService, LoginService>();
            builder.Services.AddScoped<AuthenticationStateProvider, DWStateProvider>();
            builder.Services.AddManagers();
            builder.Services.AddTransient<AuthenticationHeaderHandler>();
            builder.Services.AddHttpClient("", x =>
            {
                x.BaseAddress = new Uri(BaseConfiguration.BaseAddress);
            }).AddHttpMessageHandler<AuthenticationHeaderHandler>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseAntiforgery();

            app.MapStaticAssets();
            app.MapRazorComponents<App>()
                .AddInteractiveWebAssemblyRenderMode()
                .AddAdditionalAssemblies(typeof(Client._Imports).Assembly);

            app.Run();
        }
    }
}
