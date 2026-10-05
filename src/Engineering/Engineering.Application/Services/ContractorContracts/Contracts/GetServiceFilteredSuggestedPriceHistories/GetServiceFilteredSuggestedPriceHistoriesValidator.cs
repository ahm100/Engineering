using Engineering.Domain.Entities.ContractorContracts;

namespace Engineering.Application.Services.ContractorContracts.Contracts.GetServiceFilteredSuggestedPriceHistories;

public class GetServiceFilteredSuggestedPriceHistoriesValidator : AbstractValidator<GetServiceFilteredSuggestedPriceHistoriesRequest>
{
    public GetServiceFilteredSuggestedPriceHistoriesValidator()
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
                    ContractorContractDetailPrice>(orderBy))
            .WithMessage("Invalid OrderBy field.");
    }
}
