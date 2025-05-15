using DWShop.Application.Features.Catalog.Commands.Create;
using DWShop.Shared.Wrapper;
using Microsoft.AspNetCore.Mvc;

namespace DWShop.Service.Api.Controllers
{
    public class CatalogController : BaseApiController
    {
        [HttpPost]
        public async Task<ActionResult<Result<int>>> CreateProduct(
            [FromBody] CreateCatalogCommand createCatalogCommand)
            => Ok(await mediator.Send(createCatalogCommand));

    }
}
