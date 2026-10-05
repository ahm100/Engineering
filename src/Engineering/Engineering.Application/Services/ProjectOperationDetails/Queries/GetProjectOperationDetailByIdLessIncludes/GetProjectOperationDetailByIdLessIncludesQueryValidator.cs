namespace Engineering.Application.Services.ProjectOperationDetails.Queries.GetProjectOperationDetailByIdLessIncludes;

public class GetProjectOperationDetailByIdLessIncludesQueryValidator : AbstractValidator<GetProjectOperationDetailByIdLessIncludesQuery>
{
    public GetProjectOperationDetailByIdLessIncludesQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(ProjectOperationDetailErrors.IdIsEmpty);
    }
}
