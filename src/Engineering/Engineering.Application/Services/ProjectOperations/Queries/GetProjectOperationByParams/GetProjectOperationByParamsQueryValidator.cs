namespace Engineering.Application.Services.ProjectOperations.Queries.GetProjectOperationByParams;

public class GetProjectOperationByParamsQueryValidator : AbstractValidator<GetProjectOperationByParamsQuery>
{
    public GetProjectOperationByParamsQueryValidator()
    {
        RuleFor(oo => oo.OperationInfoId).NotNull().GreaterThanOrEqualTo(1).WithError(ProjectOperationErrors.OperationInfoIdIsEmpty);
        RuleFor(oo => oo.ProjectId).NotNull().GreaterThanOrEqualTo(1).WithError(ProjectOperationErrors.ProjectIdIsEmpty);
        RuleFor(oo => oo.MeasurementId).NotNull().GreaterThanOrEqualTo(1).WithError(ProjectOperationErrors.MeasurementIdIsEmpty);
    }
}
