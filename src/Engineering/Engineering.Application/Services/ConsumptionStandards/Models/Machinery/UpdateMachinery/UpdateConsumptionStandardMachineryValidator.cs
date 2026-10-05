namespace Engineering.Application.Services.ConsumptionStandards.Models.Machinery.UpdateMachinery;

public class UpdateConsumptionStandardMachineryValidator : AbstractValidator<UpdateConsumptionStandardMachineryRequest>
{
    public UpdateConsumptionStandardMachineryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).NotEmpty().WithError(MachineryStandardErrors.MachineryUnitIdIsEmpty);
        RuleFor(oo => oo.OperationInfoMachineryId).NotNull().GreaterThanOrEqualTo(1).NotEmpty().WithError(MachineryStandardErrors.MachineryUnitIdIsEmpty);
        RuleFor(oo => oo.MachineryNumber).GreaterThan(0).WithError(MachineryStandardErrors.MachineryNumberIsEmpty);
        RuleFor(oo => oo.TimeSpant).NotNull().WithError(MachineryStandardErrors.TimeSpantIsEmpty);
    }
}
