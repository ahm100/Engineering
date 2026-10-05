namespace Engineering.Application.Services.ContractorContracts.Contracts.GetDraftedServiceCCs;

public class GetDraftedServiceCCsValidator : AbstractValidator<GetDraftedServiceCCsRequest>
{
    public GetDraftedServiceCCsValidator()
    {
        RuleFor(c => c.ContractorId)
            .IsPositive(GlobalCmts.ContractorId);

        RuleFor(c => c.ProjectId)
            .IsPositive(GlobalCmts.ProjectId);
    }
}
