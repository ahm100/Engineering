namespace Engineering.Application.Services.Machineries.Models.InactiveMachinery;

public class InactiveMachineryValidator : AbstractValidator<InactiveMachineryRequest>
{
    public InactiveMachineryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(MachineryErrors.IdIsEmpty);
    }
}
