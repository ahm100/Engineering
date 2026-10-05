namespace Engineering.Application.Services.ContractorMachineries.Models.GetFltrByContractorIds;

public class GetFltrByContractorIdsValidator : AbstractValidator<GetFltrByContractorIdsRequest>
{
    public GetFltrByContractorIdsValidator()
    {
        RuleForEach(oo => oo.ContractorIds).IsPositive(GlobalCmts.Id);
    }
}