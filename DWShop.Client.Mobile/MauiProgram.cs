using Microsoft.Extensions.Logging;
using DWShop.Client.Infrastructure.Extensions;
using DWShop.Client.Mobile.Views;
using DWShop.Client.Mobile.Services;
using DWShop.Client.Mobile.Context;
using DWShop.Client.Infrastructure.Constants;
using DWShop.Client.Mobile.ViewModels;

namespace DWShop.Client.Mobile
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
                .Services.AddTransient<LoginView>()
                .AddTransient<BasketView>()
                .AddTransient<MainTabbedPage>()
                .AddTransient<ProductListView>()
                .AddTransient<ProductView>()
                .AddTransient<UtilityService>()
                .AddTransient<ProductRepo>()
                .AddTransient<ProductListViewModel>()
                .AddScoped(sp => new HttpClient() {  BaseAddress = new Uri(BaseConfiguration.BaseAddress) })
                .AddManagers();
                


#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
