using DWShop.Web.Infrastructure.Settings;
using MudBlazor;

namespace DWShop.Web.Client.Client.Layout
{
    public partial class MainLayout
    {


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
