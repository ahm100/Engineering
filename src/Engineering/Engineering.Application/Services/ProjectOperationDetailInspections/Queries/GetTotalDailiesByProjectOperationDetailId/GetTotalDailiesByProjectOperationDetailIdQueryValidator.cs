namespace Engineering.Application.Services.ProjectOperationDetailInspections.Queries.GetTotalDailiesByProjectOperationDetailId;

public class GetTotalDailiesByProjectOperationDetailIdQueryValidator : AbstractValidator<GetTotalDailiesByProjectOperationDetailIdQuery>
{
    public GetTotalDailiesByProjectOperationDetailIdQueryValidator()
    {
        RuleFor(oo => oo.ProjectOperationDetailId).NotNull().WithError(ProjectOperationDetailInspectionErrors.ProjectOperationDetailIdIsEmpty)
            .GreaterThan(0).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}
