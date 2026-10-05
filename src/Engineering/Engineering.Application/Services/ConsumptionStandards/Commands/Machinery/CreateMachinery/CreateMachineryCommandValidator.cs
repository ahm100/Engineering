namespace Engineering.Application.Services.ConsumptionStandards.Commands.Machinery.CreateMachinery;

public class CreateMachineryCommandValidator : AbstractValidator<CreateMachineryCommand>
{
    public CreateMachineryCommandValidator()
    {
        RuleFor(oo => oo.Machinery).NotNull().WithError(MachineryStandardErrors.MachineryUnitIdIsEmpty);
        RuleFor(oo => oo.MachineryNumber).GreaterThanOrEqualTo(0).WithError(MachineryStandardErrors.MachineryNumberIsEmpty);
        RuleFor(oo => oo.TimeSpant).NotNull().WithError(MachineryStandardErrors.TimeSpantIsEmpty);
    }
}