
namespace Engineering.Application.Services.ContractorContracts.Contracts.GetContractorContractHeaderInfo;

public class GetFilteredContractorContractHeaderInfoValidator : AbstractValidator<GetFilteredContractorContractHeaderInfoRequest>
{
    public GetFilteredContractorContractHeaderInfoValidator()
    {
        RuleFor(oo => oo.ContractorContractTypeId)
            .IsPositive(CCCmts.ContractorContractTypeId);

        RuleForEach(oo => oo.ProjectOperationDetailServiceIds)
            .IsPositive(CCCmts.ProjectOperationDetailContractorServiceId);

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
