namespace Engineering.Application.Services.ProjectOperationDetails.Queries.GetProjectOperationDetailByContractor;

public class GetProjectOperationDetailByContractorQueryValidator : AbstractValidator<GetProjectOperationDetailByContractorQuery>
{
    public GetProjectOperationDetailByContractorQueryValidator()
    {
        RuleFor(oo => oo.ProjectId).NotNull().GreaterThanOrEqualTo(1).WithError(ProjectOperationDetailErrors.IdIsEmpty);
        RuleFor(oo => oo.ContractorId).NotNull().GreaterThanOrEqualTo(1).WithError(ProjectOperationDetailErrors.IdIsEmpty);
    }
}
