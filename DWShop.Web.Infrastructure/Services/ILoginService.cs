using DWShop.Application.Features.Identity.Commands.Login;
using DWShop.Shared.Wrapper;

namespace DWShop.Web.Infrastructure.Services
{
    public interface ILoginService
    {
        Task<IResult> Login(LoginCommand command);
    }
}
