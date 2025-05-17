using DWShop.Application.Features.Basket.Commands.Delete;
using DWShop.Application.Features.Catalog.Commands.Create;
using DWShop.Application.Features.Catalog.Queries;
using DWShop.Application.Responses.Catalog;
using DWShop.Shared.Wrapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DWShop.Service.Api.Controllers
{
   
    public class CatalogController : BaseApiController
    {
        
        [HttpPost]
        public async Task<ActionResult<Result<int>>> CreateProduct(
            [FromBody] CreateCatalogCommand createCatalogCommand)
            => Ok(await mediator.Send(createCatalogCommand));

        /// <summary>
        /// Deletes a product from the catalog by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the product to delete.</param>
        /// <returns>A result indicating the success or failure of the operation.</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Result>> DeleteProduct(int id)
            => Ok(await mediator.Send(new DeleteCatalogCommand { Id = id }));


        [Authorize(Roles = "Admin2")]
        [HttpGet]
        public async Task<ActionResult<Result<IEnumerable<CatalogResponse>>>> GetAll()
            => Ok(await mediator.Send(new GetCatalogQuery()));
        
    }
}
