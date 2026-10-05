
namespace Engineering.Application.Services.ContractorContracts.Contracts.GetSuggestedServicePrice;

public class GetSuggestedServicePriceValidator : AbstractValidator<GetSuggestedServicePriceRequest>
{
    public GetSuggestedServicePriceValidator()
    {
        RuleFor(c => c.PageIndex)
            .PageIndexZero(GlobalCmts.PageIndex);
        RuleFor(c => c.PageSize)
            .PageSizeZero(GlobalCmts.PageSize);
        When(v => v.PageSize > 0, () =>
        {
            RuleFor(c => c.PageIndex)
                .PageIndexOne(GlobalCmts.PageIndex);
        });
        RuleFor(x => x.OrderBy)
            .Must(orderBy =>
                RuleExtensions.HasOnlyValidOrderFields<
                    GetSuggestedServicePriceModel>(orderBy))
            .WithMessage("Invalid OrderBy field.");
    }
}
