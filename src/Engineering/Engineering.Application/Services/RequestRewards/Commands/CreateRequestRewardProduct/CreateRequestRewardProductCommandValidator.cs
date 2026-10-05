namespace Engineering.Application.Services.RequestRewards.Commands.CreateRequestRewardProduct;

public class CreateRequestRewardProductCommandValidator : AbstractValidator<CreateRequestRewardProductCommand>
{
    public CreateRequestRewardProductCommandValidator()
    {
        RuleFor(oo => oo.Price).GreaterThan(0).NotEmpty().WithError(RequestRewardProductErrors.InValidPrice);
        RuleFor(oo => oo.Count).GreaterThan(0).NotEmpty().WithError(RequestRewardProductErrors.InValidCount);
        RuleFor(oo => oo.ProductId).NotNull().WithError(RequestRewardProductErrors.InValidProduct);
        RuleFor(oo => oo.CurrencyId).NotNull().WithError(RequestRewardProductErrors.InValidCurrency);
    }
}
