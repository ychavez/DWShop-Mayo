using AutoMapper;
using DWShop.Application.Responses.Catalog;
using DWShop.Infrastructure.Repositories;
using DWShop.Shared.Wrapper;
using MediatR;

namespace DWShop.Application.Features.Catalog.Queries
{
    public class GetCatalogQueryHandler : IRequestHandler<GetCatalogQuery, Result<IEnumerable<CatalogResponse>>>
    {
        private readonly IMapper mapper;
        private readonly IRepositoryAsync<Domain.Entities.Catalog, int> catalogRepositoryAsync;

        public GetCatalogQueryHandler(IMapper mapper, 
            IRepositoryAsync<Domain.Entities.Catalog, int> repositoryAsync)
        {
            this.mapper = mapper;
            this.catalogRepositoryAsync = repositoryAsync;
        }

        public async Task<Result<IEnumerable<CatalogResponse>>> Handle(GetCatalogQuery request, CancellationToken cancellationToken)
        {

            var catalogs = await catalogRepositoryAsync.GetAllAsync();

            var catalogResponse = mapper.Map<List<CatalogResponse>>(catalogs);

            return await Result<IEnumerable<CatalogResponse>>.SuccessAsync(catalogResponse, "");

        }
    }
}
