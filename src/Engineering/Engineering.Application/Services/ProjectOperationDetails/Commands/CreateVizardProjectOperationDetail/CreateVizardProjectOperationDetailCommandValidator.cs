
namespace Engineering.Application.Services.ProjectOperationDetails.Commands.CreateVizardProjectOperationDetail;

public class CreateVizardProjectOperationDetailCommandValidator : AbstractValidator<CreateVizardProjectOperationDetailCommand>
{
    public CreateVizardProjectOperationDetailCommandValidator()
    {
        RuleFor(oo => oo.ProjectOperation).NotEmpty().WithError(ProjectOperationDetailErrors.ProjectOperationIdIsEmpty);
        RuleFor(oo => oo.OperationLocation).NotEmpty().WithError(ProjectOperationDetailErrors.OperationLocationIdIsEmpty);
        RuleFor(oo => oo.Length).NotNull().GreaterThan(0).WithError(ProjectOperationDetailErrors.LengthIsEmpty);
        RuleFor(oo => oo.LengthChangeable).NotNull().WithError(ProjectOperationDetailErrors.LengthChangeableIsEmpty);
        RuleFor(oo => oo.Width).NotNull().GreaterThan(0).WithError(ProjectOperationDetailErrors.WidthIsEmpty);
        RuleFor(oo => oo.WidthChangeable).NotNull().WithError(ProjectOperationDetailErrors.WidthChangeableIsEmpty);
        RuleFor(oo => oo.Height).NotNull().GreaterThan(0).WithError(ProjectOperationDetailErrors.HeightIsEmpty);
        RuleFor(oo => oo.HeightChangeable).NotNull().WithError(ProjectOperationDetailErrors.HeightChangeableIsEmpty);
        RuleFor(oo => oo.Weight).NotNull().GreaterThan(0).WithError(ProjectOperationDetailErrors.WeightIsEmpty);
        RuleFor(oo => oo.WeightChangeable).NotNull().WithError(ProjectOperationDetailErrors.WeightChangeableIsEmpty);
        RuleFor(oo => oo.Number).NotNull().GreaterThan(0).WithError(ProjectOperationDetailErrors.NumberIsEmpty);
        RuleFor(oo => oo.NumberChangeable).NotNull().WithError(ProjectOperationDetailErrors.NumberChangeableIsEmpty);
        RuleFor(oo => oo.Priority).NotNull().GreaterThan(0).WithError(ProjectOperationDetailErrors.PriorityIsEmpty);
    }
}
