namespace Engineering.Application.Services.Machineries.Models.ActiveMachinery;

public class ActiveMachineryValidator : AbstractValidator<ActiveMachineryRequest>
{
    public ActiveMachineryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(MachineryErrors.IdIsEmpty);
    }
}
