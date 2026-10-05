namespace Engineering.Application.Services.ProjectOperations.Queries.GetProjectOperationByIdIncludelessNew;

public class GetProjectOperationByIdIncludelessNewQueryValidator : AbstractValidator<GetProjectOperationByIdIncludelessNewQuery>
{
    public GetProjectOperationByIdIncludelessNewQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().WithError(ProjectOperationErrors.IdIsEmpty);
    }
}
