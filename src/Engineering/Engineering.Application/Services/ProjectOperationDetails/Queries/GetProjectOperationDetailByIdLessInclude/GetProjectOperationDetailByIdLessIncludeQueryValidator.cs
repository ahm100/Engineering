namespace Engineering.Application.Services.ProjectOperationDetails.Queries.GetProjectOperationDetailByIdLessInclude;

public class GetProjectOperationDetailByIdLessIncludeQueryValidator : AbstractValidator<GetProjectOperationDetailByIdLessIncludeQuery>
{
    public GetProjectOperationDetailByIdLessIncludeQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(ProjectOperationDetailErrors.IdIsEmpty);
    }
}
