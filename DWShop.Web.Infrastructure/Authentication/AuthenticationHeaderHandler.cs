using Blazored.LocalStorage;
using DWShop.Client.Infrastructure.Constants;
using System.Net.Http.Headers;

namespace DWShop.Web.Infrastructure.Authentication
{
    public class AuthenticationHeaderHandler : DelegatingHandler
    {
        private readonly ILocalStorageService localStorageService;

        public AuthenticationHeaderHandler(ILocalStorageService localStorageService)
        {
            this.localStorageService = localStorageService;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            try
            {

                if (request.Headers.Authorization?.Scheme != BaseConfiguration.Scheme)
                {
                    var savedToken = await localStorageService.GetItemAsync<string>(BaseConfiguration.AuthToken);
                    if (!string.IsNullOrEmpty(savedToken))
                    {
                        request.Headers.Authorization = new AuthenticationHeaderValue(BaseConfiguration.Scheme, savedToken);

                    }
                }
            }
            catch (Exception)
            {

                return await base.SendAsync(request, cancellationToken);
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
