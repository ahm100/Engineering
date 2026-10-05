
namespace Engineering.Application.Services.ProjectOperationDetailDeductions.Models.CreateProjectOperationDetailDeduction;

public class CreateProjectOperationDetailDeductionValidator : AbstractValidator<CreateProjectOperationDetailDeductionRequest>
{
    public CreateProjectOperationDetailDeductionValidator()
    {
        RuleFor(oo => oo.ProjectOperationDetailId).NotNull().WithError(ProjectOperationDetailDeductionErrors.ProjectOperationDetailIdIsEmpty)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);
        RuleFor(oo => oo.Length).NotNull().WithError(ProjectOperationDetailDeductionErrors.LengthIsEmpty)
            .GreaterThan(0).WithError(ProjectOperationDetailDeductionErrors.LengthMustGreaterZiro);
        RuleFor(oo => oo.Width).NotNull().WithError(ProjectOperationDetailDeductionErrors.WidthIsEmpty)
            .GreaterThan(0).WithError(ProjectOperationDetailDeductionErrors.WidthMustGreaterZiro);
        RuleFor(oo => oo.Height).NotNull().WithError(ProjectOperationDetailDeductionErrors.HeightIsEmpty)
            .GreaterThan(0).WithError(ProjectOperationDetailDeductionErrors.HeightMustGreaterZiro);
        RuleFor(oo => oo.Weight).NotNull().WithError(ProjectOperationDetailDeductionErrors.WeightIsEmpty)
            .GreaterThan(0).WithError(ProjectOperationDetailDeductionErrors.WeightMustGreaterZiro);
        RuleFor(oo => oo.Number).NotNull().WithError(ProjectOperationDetailDeductionErrors.NumberIsEmpty)
            .GreaterThan(0).WithError(ProjectOperationDetailDeductionErrors.NumberMustGreaterZiro);
    }
}
