namespace Engineering.Application.Services.ContractorContracts.Queries.GetContractsByProjectId;

public class GetContractsByProjectIdQueryValidator : AbstractValidator<GetContractsByProjectIdQuery>
{
    public GetContractsByProjectIdQueryValidator()
    {
        RuleFor(c => c.ProjectId)
            .IsPositive(GlobalCmts.ProjectId);
    }
}