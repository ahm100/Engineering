using Engineering.Domain.Entities.ContractorContracts;

namespace Engineering.Application.Services.ContractorContracts.Contracts.GetsFilteredContractorContractDetailReports;

public class GetsFilteredContractorContractDetailReportsValidator : AbstractValidator<GetsFilteredContractorContractDetailReportsRequest>
{
    public GetsFilteredContractorContractDetailReportsValidator()
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
                    ContractorContractDetail>(orderBy))
            .WithMessage("Invalid OrderBy field.");
    }
}
