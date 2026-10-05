namespace Engineering.Application.Services.ConsumableVolumes.Commands.Machineries.DeleteMachineryConsumableVolume;

public class DeleteConsumableVolumeMachineryCommandValidator : AbstractValidator<DeleteConsumableVolumeMachineryCommand>
{
    public DeleteConsumableVolumeMachineryCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().WithError(ConsumableVolumeMachineryErrors.IdIsEmpty);
    }
}
