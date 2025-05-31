using CommunityToolkit.Mvvm.Messaging;
using DWShop.Client.Infrastructure.Constants;
using DWShop.Client.Infrastructure.Managers.Login;
using DWShop.Client.Mobile.Messages;
using DWShop.Client.Mobile.Models;
using DWShop.Client.Mobile.ViewModels.Base;
using DWShop.Client.Mobile.Views;
using System.Windows.Input;

namespace DWShop.Client.Mobile.ViewModels
{
    public class LoginViewModel: BaseViewModel
    {
        private readonly IAuthenticationManager authenticationManager;
        private LoginModel loginModel;
        public LoginModel LoginModel 
        {
            get => loginModel;
            set => SetProperty(ref loginModel, value);
        }
        public ICommand LoginCommand { get; set; }

        public LoginViewModel(IAuthenticationManager authenticationManager, 
            HttpClient httpClient, MainTabbedPage mainTabbedPage)
        {
            loginModel = new();
            this.authenticationManager = authenticationManager;

            LoginCommand = new Command(async () =>
            {

                var result = await authenticationManager.Login(new()
                {
                    Password = loginModel.Password,
                    UserName = loginModel.UserName
                });

                if (result.Succeded)
                {
                    await SecureStorage.Default.SetAsync("Token", result.Data.Token);

                    httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue(BaseConfiguration.Scheme, result.Data.Token);

                    WeakReferenceMessenger.Default.Send(new ProductListRefreshMessage());

                    Microsoft.Maui.Controls.Application.Current.MainPage = new NavigationPage(mainTabbedPage);

                }

            });
        }
    }
}
