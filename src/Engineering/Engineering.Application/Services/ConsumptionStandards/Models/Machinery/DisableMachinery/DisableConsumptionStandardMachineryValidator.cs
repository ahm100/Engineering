namespace Engineering.Application.Services.ConsumptionStandards.Models.Machinery.DisableMachinery;

public class DisableConsumptionStandardMachineryValidator : AbstractValidator<DisableConsumptionStandardMachineryRequest>
{
    public DisableConsumptionStandardMachineryValidator()
    {
        RuleFor(oo => oo.OperationInfoMachineryId).NotNull().WithError(MachineryStandardErrors.MachineryIdIsEmpty);
    }
}
