using DWShop.Application.Features.Basket.Commands.Create;
using FluentValidation;

namespace DWShop.Application.Validations.Basket.Create
{
    public class CreateBasketCommandValidator : AbstractValidator<CreateBasketCommand>
    {
        public CreateBasketCommandValidator()
        {
            RuleFor(x => x.TotalPrice).GreaterThan(0);
        }
    }
}
