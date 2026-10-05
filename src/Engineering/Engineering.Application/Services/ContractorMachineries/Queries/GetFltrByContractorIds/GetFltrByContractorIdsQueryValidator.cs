namespace Engineering.Application.Services.ContractorMachineries.Queries.GetFltrByContractorIds;

public class GetFltrByContractorIdsQueryValidator : AbstractValidator<GetFltrByContractorIdsQuery>
{
    public GetFltrByContractorIdsQueryValidator()
    {
        RuleForEach(oo => oo.ContractorIds).IsPositive(GlobalCmts.Id);
    }
}