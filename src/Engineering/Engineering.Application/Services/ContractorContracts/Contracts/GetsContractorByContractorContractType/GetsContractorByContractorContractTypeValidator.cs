
namespace Engineering.Application.Services.ContractorContracts.Contracts.GetsContractorByContractorContractType;

public class GetsContractorByContractorContractTypeValidator : AbstractValidator<GetsContractorByContractorContractTypeRequest>
{
    public GetsContractorByContractorContractTypeValidator()
    {
        RuleFor(oo => oo.ContractorContractTypeId)
            .IsPositive(CCCmts.ContractorContractTypeId);

        RuleFor(c => c.PageIndex)
            .PageIndexZero(GlobalCmts.PageIndex);
        RuleFor(c => c.PageSize)
            .PageSizeZero(GlobalCmts.PageSize);
        When(v => v.PageSize > 0, () =>
        {
            RuleFor(c => c.PageIndex)
                .PageIndexOne(GlobalCmts.PageIndex);
        });
    }
}
