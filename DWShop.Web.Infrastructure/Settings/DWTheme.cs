using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DWShop.Web.Infrastructure.Settings
{
    public static class DWTheme
    {


        public static MudTheme DefaultTheme = new()
        {
            PaletteLight = new()
            {
                Primary = new MudBlazor.Utilities.MudColor("#CDDC39")

            }

        };


        public static MudTheme DarkTheme = new()
        {
            PaletteDark = new()
            {
                Primary = new MudBlazor.Utilities.MudColor("#1E88E5")

            }

        };
    }
}
