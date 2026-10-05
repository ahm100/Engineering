
namespace Engineering.Application.Services.Machineries.Commands.ActiveMachinery;

public class ActiveMachineryCommandValidator : AbstractValidator<ActiveMachineryCommand>
{
    public ActiveMachineryCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(MachineryErrors.IdIsEmpty);
    }
}