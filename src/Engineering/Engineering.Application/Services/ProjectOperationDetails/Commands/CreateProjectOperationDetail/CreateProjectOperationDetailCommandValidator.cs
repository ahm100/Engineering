
namespace Engineering.Application.Services.ProjectOperationDetails.Commands.CreateProjectOperationDetail;

public class CreateProjectOperationDetailCommandValidator : AbstractValidator<CreateProjectOperationDetailCommand>
{
    public CreateProjectOperationDetailCommandValidator()
    {
        RuleFor(oo => oo.ProjectOperation)
            .NotEmpty().WithError(ProjectOperationDetailErrors.ProjectOperationIdIsEmpty);

        RuleFor(oo => oo.OperationLocation)
            .NotEmpty().WithError(ProjectOperationDetailErrors.OperationLocationIdIsEmpty);

        RuleFor(oo => oo.Length)
            .NotNull().WithError(ProjectOperationDetailErrors.LengthIsEmpty)
            .GreaterThan(0).WithError(ProjectOperationDetailErrors.LengthIsEmpty);

        RuleFor(oo => oo.LengthChangeable)
            .NotNull().WithError(ProjectOperationDetailErrors.LengthChangeableIsEmpty);

        RuleFor(oo => oo.Width)
            .NotNull().WithError(ProjectOperationDetailErrors.WidthIsEmpty)
            .GreaterThan(0).WithError(ProjectOperationDetailErrors.WidthIsLow);

        RuleFor(oo => oo.WidthChangeable)
            .NotNull().WithError(ProjectOperationDetailErrors.WidthChangeableIsEmpty);

        RuleFor(oo => oo.Height)
            .NotNull().WithError(ProjectOperationDetailErrors.HeightIsEmpty)
            .GreaterThan(0).WithError(ProjectOperationDetailErrors.HeightIsLow);

        RuleFor(oo => oo.HeightChangeable)
            .NotNull().WithError(ProjectOperationDetailErrors.HeightChangeableIsEmpty);

        RuleFor(oo => oo.Weight)
            .NotNull().WithError(ProjectOperationDetailErrors.WeightIsEmpty)
            .GreaterThan(0).WithError(ProjectOperationDetailErrors.WeightIsLow);

        RuleFor(oo => oo.WeightChangeable)
            .NotNull().WithError(ProjectOperationDetailErrors.WeightChangeableIsEmpty);

        RuleFor(oo => oo.Number)
            .NotNull().WithError(ProjectOperationDetailErrors.NumberIsEmpty)
            .GreaterThan(0).WithError(ProjectOperationDetailErrors.NumberIsLow);

        RuleFor(oo => oo.NumberChangeable)
            .NotNull().WithError(ProjectOperationDetailErrors.NumberChangeableIsEmpty);

        RuleFor(oo => oo.Priority)
            .NotNull().WithError(ProjectOperationDetailErrors.PriorityIsEmpty)
            .GreaterThan(0).WithError(ProjectOperationDetailErrors.PriorityIsLow);
    }
}