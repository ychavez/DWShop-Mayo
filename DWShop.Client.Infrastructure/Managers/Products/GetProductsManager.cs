using DWShop.Application.Responses.Catalog;
using DWShop.Client.Infrastructure.Extensions;
using DWShop.Client.Infrastructure.Routes.Products;
using DWShop.Shared.Wrapper;

namespace DWShop.Client.Infrastructure.Managers.Products
{
    public class GetProductsManager : IGetProductsManager
    {
        private readonly HttpClient httpClient;

        public GetProductsManager(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IResult<IEnumerable<CatalogResponse>>> GetAllProducts()
        {
            var response = await httpClient.GetAsync(ProductsEndpoints.GetAllProducts); 
            var data = await response.ToResult<IEnumerable<CatalogResponse>>();
            return data;
        }
    }
}
