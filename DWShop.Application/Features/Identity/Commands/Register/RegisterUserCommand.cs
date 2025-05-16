using DWShop.Shared.Wrapper;
using MediatR;

namespace DWShop.Application.Features.Identity.Commands.Register
{
    public class RegisterUserCommand : IRequest<Result<string>>
    {
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
    }
}
