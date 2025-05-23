using DWShop.Web.Infrastructure.Services;
using DWShop.Web.Infrastructure.Settings;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace DWShop.Web.Client.Components.Layout
{
    public partial class MainLayout
    {

        [Inject]
        ClientPreferencesServices _clientPreferences { get; set; }

        private MudTheme currentTheme = DWTheme.DefaultTheme;

        bool isDarkMode;


        public async Task DarkMode()
        {
            isDarkMode = await _clientPreferences.ToogleDarkModeAsync();

            currentTheme = isDarkMode ?
                  DWTheme.DefaultTheme :
                  DWTheme.DarkTheme;
        }

        void ToggleDrawer() 
        {
            navOpen = !navOpen;
        }
    }
}
