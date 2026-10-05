namespace Engineering.Application.Services.RequestMachineries.Commands.DeleteRequestMachinery;

public class DeleteRequestMachineryCommandValidator : AbstractValidator<DeleteRequestMachineryCommand>
{
    public DeleteRequestMachineryCommandValidator()
    {
        RuleFor(oo => oo.RequestMachineryId).NotNull().WithError(RequestMachineryErrors.InValidRequestMachinery);
    }
}
