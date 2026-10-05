namespace Engineering.Application.Services.ProjectOperations.Queries.GetProjectOperationByIdsLessInclude;

public class GetProjectOperationByIdsLessIncludeQueryValidator : AbstractValidator<GetProjectOperationByIdsLessIncludeQuery>
{
    public GetProjectOperationByIdsLessIncludeQueryValidator()
    {
        RuleFor(oo => oo.Ids).NotEmpty().WithError(ProjectOperationErrors.IdIsEmpty);
    }
}
