using AutoMapper;
using MediatR;
using DWShop.Infrastructure.Repositories;
using DWShop.Domain.Entities;

namespace DWShop.Application.Features.Catalog.Commands.Create
{
    public class CreateCatalogCommandHandler : IRequestHandler<CreateCatalogCommand, int>
    {
        private readonly IMapper mapper;
        private readonly IRepositoryAsync<Domain.Entities.Catalog, int> repositoryAsync;

        public CreateCatalogCommandHandler(IMapper mapper, 
            IRepositoryAsync<Domain.Entities.Catalog,int> repositoryAsync)
        {
            this.mapper = mapper;
            this.repositoryAsync = repositoryAsync;
        }
        public async Task<int> Handle(CreateCatalogCommand request, CancellationToken cancellationToken)
        {
            var catalogDb = await repositoryAsync.GetPagedAsync(1, 1, x => x.Name == request.Name, default);

            if (catalogDb.Any())
            {
                throw new Exception($"El producto {request.Name} ya se encuentra en la base de datos");
            }

            var catalog = mapper.Map<Domain.Entities.Catalog>(request);

            await repositoryAsync.AddAsync(catalog);

            await repositoryAsync.SaveChangesAsync();

            return catalog.Id;
        }
    }
}
