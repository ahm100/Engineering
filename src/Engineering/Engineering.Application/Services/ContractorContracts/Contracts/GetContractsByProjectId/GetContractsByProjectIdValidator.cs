namespace Engineering.Application.Services.ContractorContracts.Contracts.GetContractsByProjectId;

public class GetContractsByProjectIdValidator : AbstractValidator<GetContractsByProjectIdRequest>
{
    public GetContractsByProjectIdValidator()
    {
        RuleFor(c => c.ProjectId)
            .IsPositive(GlobalCmts.ProjectId);
    }
}