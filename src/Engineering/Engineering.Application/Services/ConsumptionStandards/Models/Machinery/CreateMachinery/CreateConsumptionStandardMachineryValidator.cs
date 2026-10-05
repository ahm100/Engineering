namespace Engineering.Application.Services.ConsumptionStandards.Models.Machinery.CreateMachinery;

public class CreateConsumptionStandardMachineryValidator : AbstractValidator<CreateConsumptionStandardMachineryRequest>
{
    public CreateConsumptionStandardMachineryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).NotEmpty().WithError(MachineryStandardErrors.MachineryUnitIdIsEmpty);
        RuleFor(oo => oo.MachineryNumber).GreaterThan(0).WithError(MachineryStandardErrors.MachineryNumberIsEmpty);
        RuleFor(oo => oo.TimeSpant).NotNull().WithError(MachineryStandardErrors.TimeSpantIsEmpty);
    }
}
