using AutoMapper;
using DWShop.Application.Features.Identity.Commands.Login;
using DWShop.Application.Responses.Identity;
using DWShop.Infrastructure.Services;
using DWShop.Shared.Wrapper;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace DWShop.Application.Features.Identity.Commands.Register
{
    public class RegisterUserCommandHanlder : IRequestHandler<RegisterUserCommand, Result<LoginResponse>>
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly IAccountService accountService;
        private readonly IMapper mapper;
        private readonly IMediator mediator;
        private readonly RoleManager<IdentityRole> roleManager;

        public RegisterUserCommandHanlder(UserManager<IdentityUser> userManager, IAccountService accountService,
            IMapper mapper, IMediator mediator, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.accountService = accountService;
            this.mapper = mapper;
            this.mediator = mediator;
            this.roleManager = roleManager;
        }


        public async Task<Result<LoginResponse>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var user = mapper.Map<IdentityUser>(request);

            if (await accountService.UserExists(request.UserName))
            {
                return await Result<LoginResponse>.FailAsync("El usuario ya existe");
            }

            user.Id = Guid.NewGuid().ToString();

            var result = await userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                return await Result<LoginResponse>.FailAsync(result.Errors.Select(x => x.Description).ToList());
            }

            // creamos el rol
            if ((await roleManager.FindByNameAsync("Admin")) is  null)
            {
                await roleManager.CreateAsync(new() { Name = "Admin" });

            }
          //  userManager.GetRolesAsync()
            await userManager.AddToRoleAsync(user, "Admin");

            var loginCommand = new LoginCommand
            { Password = request.Password, UserName = request.UserName };

            return await mediator.Send(loginCommand);
        }
    }
}


/*
 *  Practica 4 puntos
 *1.-  crear un edpoint que de de alta Roles, solo los que esten en el Rol de admin podran consumir este endpoint
 *  (tomar en cuenta validaciones)
 *  
 * 2.- crear endpoint para agregar usuario a rol (Validaciones)
 * 
 * 3.- hacer un endpoint que remueva de roles al usuario parametros (rol, usuario)
 * 
 * 4.- hacer endpoint que obtenga los roles del usuario (no solo devolver lista de strings)
 * 
 * 
 * 
 * 
 */