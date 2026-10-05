namespace Engineering.Application.Services.ProjectOperations.Queries.GetProjectOperationById;

public class GetProjectOperationByIdQueryValidator : AbstractValidator<GetProjectOperationByIdQuery>
{
    public GetProjectOperationByIdQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().WithError(ProjectOperationErrors.IdIsEmpty);
    }
}