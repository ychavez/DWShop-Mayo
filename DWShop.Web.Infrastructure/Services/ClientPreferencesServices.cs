using Blazored.LocalStorage;
using DWShop.Web.Infrastructure.Settings;
using MudBlazor;

namespace DWShop.Web.Infrastructure.Services
{
    public class ClientPreferencesServices
    {
        private readonly ILocalStorageService localStorageService;

        public ClientPreferencesServices(ILocalStorageService localStorageService)
        {
            this.localStorageService = localStorageService;
        }

        public async Task<MudTheme> GetCurrentThemeAsync()
        {
            var preferences = await GetPreferences();
            if (preferences is ClientPreference preference)
            {
                if (preference.IsDarkMode)
                {
                    return DWTheme.DarkTheme;
                }
            }

            return DWTheme.DefaultTheme;
        }

        public async Task<bool> ToogleDarkModeAsync() 
        {
            var preferences = await GetPreferences() as ClientPreference;

            if (preferences is not null)
            {
                preferences.IsDarkMode = !preferences.IsDarkMode;
                await localStorageService.
                    SetItemAsync<ClientPreference>("clientPreferences", preferences);

                return !preferences.IsDarkMode;
            }
            return false;
        }

        public async Task<IPreference> GetPreferences() 
        {
            var preferences = await
                localStorageService.
                GetItemAsync<ClientPreference>("clientPreferences")
                ?? new ClientPreference();

            return preferences;
        }
    }

    public interface IPreference { }

    public record ClientPreference : IPreference 
    {
        public bool IsDarkMode { get; set; }
        // agregar las demas preferencias
    }
}
