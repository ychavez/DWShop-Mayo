using DWShop.Shared.Wrapper;
using MediatR;

namespace DWShop.Application.Features.Basket.Commands.Create
{
    public class CreateBasketCommand: IRequest<IResult<int>>
    {
        public decimal TotalPrice { get; set; }
    }
}
