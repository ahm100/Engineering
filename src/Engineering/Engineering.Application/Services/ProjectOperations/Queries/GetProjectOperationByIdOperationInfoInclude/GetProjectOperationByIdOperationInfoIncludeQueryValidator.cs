
namespace Engineering.Application.Services.ProjectOperations.Queries.GetProjectOperationByIdOperationInfoInclude;

public class GetProjectOperationByIdOperationInfoIncludeQueryValidator : AbstractValidator<GetProjectOperationByIdOperationInfoIncludeQuery>
{
    public GetProjectOperationByIdOperationInfoIncludeQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().WithError(ProjectOperationErrors.IdIsEmpty);
    }
}
