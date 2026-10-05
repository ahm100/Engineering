namespace Engineering.Application.Services.ProjectOperationDetails.Queries.GetProjectOperationDetailWithStandards;

public class GetProjectOperationDetailWithStandardsQueryValidator : AbstractValidator<GetProjectOperationDetailWithStandardsQuery>
{
    public GetProjectOperationDetailWithStandardsQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(ProjectOperationDetailErrors.IdIsEmpty);
    }
}
