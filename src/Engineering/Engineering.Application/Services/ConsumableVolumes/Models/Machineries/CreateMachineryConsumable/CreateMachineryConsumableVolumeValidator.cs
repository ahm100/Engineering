namespace Engineering.Application.Services.ConsumableVolumes.Models.Machineries.CreateMachineryConsumable;

public class CreateConsumableVolumeMachineryValidator : AbstractValidator<CreateConsumableVolumeMachineryRequest>
{
    public CreateConsumableVolumeMachineryValidator()
    {
        RuleFor(oo => oo.ProjectOperationDetailId).NotNull().GreaterThanOrEqualTo(1).WithError(ConsumableVolumeMachineryErrors.ProjectOperationDetailIdIsEmpty);
        RuleFor(oo => oo.MachineryId).NotNull().GreaterThanOrEqualTo(1).WithError(ConsumableVolumeMachineryErrors.MachineryIsEmpty);
        RuleFor(oo => oo.FinalValue).NotEmpty().WithError(ConsumableVolumeMachineryErrors.FinalValueIsEmpty);
    }
}
