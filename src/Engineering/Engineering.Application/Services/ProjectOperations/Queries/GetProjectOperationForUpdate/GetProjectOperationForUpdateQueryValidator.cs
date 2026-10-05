namespace Engineering.Application.Services.ProjectOperations.Queries.GetProjectOperationForUpdate;

public class GetProjectOperationForUpdateQueryValidator : AbstractValidator<GetProjectOperationForUpdateQuery>
{
    public GetProjectOperationForUpdateQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().WithError(ProjectOperationErrors.IdIsEmpty);
    }
}
