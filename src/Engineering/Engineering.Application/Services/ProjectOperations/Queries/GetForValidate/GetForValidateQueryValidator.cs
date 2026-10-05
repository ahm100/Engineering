namespace Engineering.Application.Services.ProjectOperations.Queries.GetForValidate;

public class GetProjectOperationForValidateQueryValidator : AbstractValidator<GetProjectOperationForValidateQuery>
{
    public GetProjectOperationForValidateQueryValidator()
    {
        RuleFor(oo => oo.ProjectId).NotNull().WithError(ProjectOperationErrors.ProjectIdIsEmpty);
        RuleFor(oo => oo.UnitOfMeasurementId).NotNull().WithError(ProjectOperationErrors.MeasurementIdIsEmpty);
    }
}
