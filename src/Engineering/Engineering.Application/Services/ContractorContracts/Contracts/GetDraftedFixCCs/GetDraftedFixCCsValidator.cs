namespace Engineering.Application.Services.ContractorContracts.Contracts.GetDraftedFixCCs;

public class GetDraftedFixCCsValidator : AbstractValidator<GetDraftedFixCCsRequest>
{
    public GetDraftedFixCCsValidator()
    {
        RuleFor(c => c.ContractorId)
            .IsPositive(GlobalCmts.ContractorId);

        RuleFor(c => c.ProjectId)
            .IsPositive(GlobalCmts.ProjectId);
    }
}
