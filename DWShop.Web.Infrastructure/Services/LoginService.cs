using Blazored.LocalStorage;
using DWShop.Application.Features.Identity.Commands.Login;
using DWShop.Application.Responses.Identity;
using DWShop.Client.Infrastructure.Managers.Login;
using DWShop.Shared.Wrapper;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DWShop.Web.Infrastructure.Services
{
    public class LoginService : ILoginService
    {
        private readonly IAuthenticationManager authenticationManager;
        private readonly ILocalStorageService localStorageService;
        private readonly AuthenticationStateProvider authenticationStateProvider;
        private readonly HttpClient httpClient;

        public LoginService(IAuthenticationManager authenticationManager,
            ILocalStorageService localStorageService,
            AuthenticationStateProvider authenticationStateProvider,
            HttpClient httpClient)
        {
            this.authenticationManager = authenticationManager;
            this.localStorageService = localStorageService;
            this.authenticationStateProvider = authenticationStateProvider;
            this.httpClient = httpClient;
        }


        public async Task<IResult> Login(LoginCommand command)
        {
            IResult<LoginResponse> result;
            try
            {
                result = await authenticationManager.Login(command);
            }
            catch (Exception)
            {

                return await Result.FailAsync();
            }
            if (result.Succeded)
            {
                await localStorageService.SetItemAsStringAsync("authToken", result.Data.Token);

                httpClient.DefaultRequestHeaders.Authorization =
                  new AuthenticationHeaderValue("Bearer", result.Data.Token);

                return await Result<Object>.SuccessAsync(new { },"");
            }

            return await Result.FailAsync(result.Messages);

        }
    }
}
