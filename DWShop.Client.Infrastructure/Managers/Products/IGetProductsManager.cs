using DWShop.Application.Responses.Catalog;
using DWShop.Client.Infrastructure.Managers.Base;
using DWShop.Shared.Wrapper;

namespace DWShop.Client.Infrastructure.Managers.Products
{
    public interface IGetProductsManager : IManager
    {
        Task<IResult<IEnumerable<CatalogResponse>>> GetAllProducts();
    }
}
