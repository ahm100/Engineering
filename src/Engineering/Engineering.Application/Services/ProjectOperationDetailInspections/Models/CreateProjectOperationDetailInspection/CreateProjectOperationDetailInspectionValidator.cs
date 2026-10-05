namespace Engineering.Application.Services.ProjectOperationDetailInspections.Models.CreateProjectOperationDetailInspection;

public class CreateProjectOperationDetailInspectionValidator : AbstractValidator<CreateProjectOperationDetailInspectionRequest>
{
    public CreateProjectOperationDetailInspectionValidator()
    {
        RuleFor(oo => oo.Length).NotNull().WithError(ProjectOperationDetailInspectionErrors.LengthIsEmpty)
           .GreaterThan(0).WithError(ProjectOperationDetailInspectionErrors.LengthMustGreaterZiro);
        RuleFor(oo => oo.Width).NotNull().WithError(ProjectOperationDetailInspectionErrors.WidthIsEmpty)
            .GreaterThan(0).WithError(ProjectOperationDetailInspectionErrors.WidthMustGreaterZiro);
        RuleFor(oo => oo.Height).NotNull().WithError(ProjectOperationDetailInspectionErrors.HeightIsEmpty)
            .GreaterThan(0).WithError(ProjectOperationDetailInspectionErrors.HeightMustGreaterZiro);
        RuleFor(oo => oo.Weight).NotNull().WithError(ProjectOperationDetailInspectionErrors.WeightIsEmpty)
            .GreaterThan(0).WithError(ProjectOperationDetailInspectionErrors.WeightMustGreaterZiro);
        RuleFor(oo => oo.Number).NotNull().WithError(ProjectOperationDetailInspectionErrors.NumberIsEmpty)
            .GreaterThan(0).WithError(ProjectOperationDetailInspectionErrors.NumberMustGreaterZiro);
        RuleFor(oo => oo.projectId).NotNull().WithError(ProjectOperationDetailInspectionErrors.ProjectIdIsEmpty)
            .GreaterThan(0).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}
