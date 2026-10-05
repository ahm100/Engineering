namespace Engineering.Application.Services.ProjectOperations.Queries.GetProjectOperationByIdLessInclude;

public class GetProjectOperationByIdLessIncludeQueryValidator : AbstractValidator<GetProjectOperationByIdLessIncludeQuery>
{
    public GetProjectOperationByIdLessIncludeQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().WithError(ProjectOperationErrors.IdIsEmpty);
    }
}
