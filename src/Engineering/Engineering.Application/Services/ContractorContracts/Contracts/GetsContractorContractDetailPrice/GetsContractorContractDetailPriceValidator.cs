namespace Engineering.Application.Services.ContractorContracts.Contracts.GetsContractorContractDetailPrice;

public class GetsContractorContractDetailPriceValidator : AbstractValidator<GetsContractorContractDetailPriceRequest>
{
    public GetsContractorContractDetailPriceValidator()
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
                    GetsContractorContractDetailPriceModel>(orderBy))
            .WithMessage("Invalid OrderBy field.");
    }
}
