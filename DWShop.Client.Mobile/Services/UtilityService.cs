using System.Net.Http.Headers;

namespace DWShop.Client.Mobile.Services
{
    public class UtilityService
    {
        private readonly HttpClient httpClient;

        public UtilityService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<bool> IsAuthenticated() 
        {
            var token = await SecureStorage.Default.GetAsync("token");

            if (token is null)
                return false;

            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var response = await httpClient.GetAsync("/api/Identity/Check");

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                httpClient.DefaultRequestHeaders.Authorization = null;
                return false;
            }

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }

            if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                // mostrar mensaje al usuario
                return false;
            }

            return false;
        }
    }
}
