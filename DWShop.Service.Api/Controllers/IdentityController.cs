using DWShop.Application.Features.Identity.Commands.Login;
using DWShop.Application.Features.Identity.Commands.Register;
using DWShop.Application.Responses.Identity;
using DWShop.Shared.Wrapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DWShop.Service.Api.Controllers
{
    public class IdentityController : BaseApiController
    {
        [HttpPost("register")]
        public async Task<ActionResult<Result<LoginResponse>>> Register([FromBody] RegisterUserCommand command)
            => Ok(await mediator.Send(command));

        [HttpPost("login")]
        public async Task<ActionResult<Result<LoginResponse>>> Login([FromBody] LoginCommand command)
            => Ok(await mediator.Send(command));

        [HttpGet("Check")]
        [Authorize]
        public ActionResult Check() => Ok(); //HealtCheck
    }
}
