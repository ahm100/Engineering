namespace Engineering.Application.Services.ProjectOperationDetailInspections.Models.GetTotalDailiesByProjectOperationDetailId;

public class GetTotalDailiesByProjectOperationDetailIdValidator : AbstractValidator<GetTotalDailiesByProjectOperationDetailIdRequest>
{
    public GetTotalDailiesByProjectOperationDetailIdValidator()
    {
        RuleFor(oo => oo.ProjectOperationDetailId).NotNull().WithError(ProjectOperationDetailInspectionErrors.ProjectOperationDetailIdIsEmpty)
            .GreaterThan(0).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}
