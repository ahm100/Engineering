namespace Engineering.Application.Services.Machineries.Models.DisableMachinery;

public class DisableMachineryValidator : AbstractValidator<DisableMachineryRequest>
{
    public DisableMachineryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(MachineryErrors.IdIsEmpty);
    }
}
