namespace Engineering.Application.Services.ProjectOperations.Models.GetProjectOperationByParams;

public class GetProjectOperationByParamsValidator : AbstractValidator<GetProjectOperationByParamsRequest>
{
    public GetProjectOperationByParamsValidator()
    {
        RuleFor(oo => oo.OperationInfoId).NotNull().GreaterThanOrEqualTo(1).WithError(ProjectOperationErrors.OperationInfoIdIsEmpty);
        RuleFor(oo => oo.ProjectId).NotNull().GreaterThanOrEqualTo(1).WithError(ProjectOperationErrors.ProjectIdIsEmpty);
        RuleFor(oo => oo.MeasurementId).NotNull().GreaterThanOrEqualTo(1).WithError(ProjectOperationErrors.MeasurementIdIsEmpty);
    }
}
