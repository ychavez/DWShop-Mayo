using DWShop.Application.Features.Basket.Commands.Create;
using DWShop.Shared.Wrapper;
using Microsoft.AspNetCore.Mvc;

namespace DWShop.Service.Api.Controllers
{
    public class BasketController : BaseApiController
    {
        [HttpPost]
        public async Task<ActionResult<Result<int>>> CreateBasket([FromBody] CreateBasketCommand createBasketCommand)
            => Ok(await mediator.Send(createBasketCommand));

    }
}
