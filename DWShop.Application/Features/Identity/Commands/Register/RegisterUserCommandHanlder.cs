using AutoMapper;
using DWShop.Infrastructure.Services;
using DWShop.Shared.Wrapper;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace DWShop.Application.Features.Identity.Commands.Register
{
    public class RegisterUserCommandHanlder : IRequestHandler<RegisterUserCommand, Result<string>>
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly IAccountService accountService;
        private readonly IMapper mapper;

        public RegisterUserCommandHanlder(UserManager<IdentityUser> userManager, IAccountService accountService,
            IMapper mapper)
        {
            this.userManager = userManager;
            this.accountService = accountService;
            this.mapper = mapper;
        }


        public Task<Result<string>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
