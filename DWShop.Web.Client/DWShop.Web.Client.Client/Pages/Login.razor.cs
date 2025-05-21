using DWShop.Application.Features.Identity.Commands.Login;
using DWShop.Web.Infrastructure.Services;
using Microsoft.AspNetCore.Components;

namespace DWShop.Web.Client.Client.Pages
{
    public partial class Login
    {
        private LoginCommand _TokenModel = new();

        [Inject]
        private ILoginService LoginService { get; set; }


        private async Task SubmitAsync()
        {
            var result = await LoginService.Login(_TokenModel);
        }
    }
}
