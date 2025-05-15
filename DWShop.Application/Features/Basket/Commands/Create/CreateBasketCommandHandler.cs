using AutoMapper;
using DWShop.Infrastructure.Repositories;
using DWShop.Shared.Wrapper;
using MediatR;

namespace DWShop.Application.Features.Basket.Commands.Create
{
    public class CreateBasketCommandHandler(IMapper mapper, IRepositoryAsync<Domain.Entities.Basket,int> repositoryAsync ) 
        : IRequestHandler<CreateBasketCommand, IResult<int>>
    {
        public async Task<IResult<int>> Handle(CreateBasketCommand request, CancellationToken cancellationToken)
        {
            var basket = mapper.Map<Domain.Entities.Basket>(request);
            basket.UserName = "User";

            await repositoryAsync.AddAsync(basket);

            await repositoryAsync.SaveChangesAsync();

            return await Result<int>.SuccessAsync(basket.Id, "");
        }
    }
}
