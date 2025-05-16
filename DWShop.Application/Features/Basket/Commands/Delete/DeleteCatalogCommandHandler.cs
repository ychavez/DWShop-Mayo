using DWShop.Domain.Entities;
using DWShop.Infrastructure.Repositories;
using DWShop.Shared.Wrapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DWShop.Application.Features.Basket.Commands.Delete
{
    public class DeleteCatalogCommandHandler(IRepositoryAsync<Domain.Entities.Catalog, int> repositoryAsync) : IRequestHandler<DeleteCatalogCommand, IResult>
    {
        public async Task<IResult> Handle(DeleteCatalogCommand request, CancellationToken cancellationToken)
        {
            var catalog = await repositoryAsync.GetByIdAsync(request.Id);
            if (catalog == null)
            {
                return Result.Fail("Catalog not found");
            }

            await repositoryAsync.DeleteAsync(catalog);
            await repositoryAsync.SaveChangesAsync();

            return Result.Success("Catalog deleted successfully");
        }
    }
}
