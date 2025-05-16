using AutoMapper;
using MediatR;
using DWShop.Infrastructure.Repositories;
using DWShop.Domain.Entities;
using DWShop.Shared.Wrapper;

namespace DWShop.Application.Features.Catalog.Commands.Create
{
    public class CreateCatalogCommandHandler : IRequestHandler<CreateCatalogCommand, IResult<int>>
    {
        private readonly IMapper mapper;
        private readonly IRepositoryAsync<Domain.Entities.Catalog, int> catalogRepositoryAsync;

        public CreateCatalogCommandHandler(IMapper mapper, 
            IRepositoryAsync<Domain.Entities.Catalog,int> repositoryAsync)
        {
            this.mapper = mapper;
            this.catalogRepositoryAsync = repositoryAsync;
        }

        public async Task<IResult<int>> Handle(CreateCatalogCommand request, CancellationToken cancellationToken)
        {

            var catalogDb = await catalogRepositoryAsync.GetPagedAsync(1, 1, x => x.Name == request.Name, default);

            if (catalogDb.Any())
            {
                throw new Exception($"El producto {request.Name} ya se encuentra en la base de datos");
            }

           var catalog = mapper.Map<Domain.Entities.Catalog>(request);

           await catalogRepositoryAsync.AddAsync(catalog);

            await catalogRepositoryAsync.SaveChangesAsync();

            return await Result<int>.SuccessAsync(catalog.Id, "");
        }
    }
}
