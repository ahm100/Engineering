namespace Engineering.Application.Services.ConsumableVolumes.Commands.Machineries.CreateMachineryConsumableVolume;

public class CreateConsumableVolumeMachineryCommandValidator : AbstractValidator<CreateConsumableVolumeMachineryCommand>
{
    public CreateConsumableVolumeMachineryCommandValidator()
    {
        RuleFor(oo => oo.ProjectOperationDetail).NotNull().WithError(ConsumableVolumeMachineryErrors.ProjectOperationDetailIdIsEmpty);
        RuleFor(oo => oo.Machinery).NotNull().WithError(ConsumableVolumeMachineryErrors.MachineryIsEmpty);
        RuleFor(oo => oo.FinalValue).NotNull().WithError(ConsumableVolumeMachineryErrors.FinalValueIsEmpty);
        RuleFor(oo => oo.IsStandard).NotNull().WithError(ConsumableVolumeMachineryErrors.IsStandardIsEmpty);
    }
}