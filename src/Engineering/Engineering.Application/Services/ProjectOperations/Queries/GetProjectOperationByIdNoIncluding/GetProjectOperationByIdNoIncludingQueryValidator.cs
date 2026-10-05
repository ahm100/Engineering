
namespace Engineering.Application.Services.ProjectOperations.Queries.GetProjectOperationByIdNoIncluding;

public class GetProjectOperationByIdNoIncludingQueryValidator : AbstractValidator<GetProjectOperationByIdNoIncludingQuery>
{
    public GetProjectOperationByIdNoIncludingQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().WithError(ProjectOperationErrors.IdIsEmpty);
    }
}
