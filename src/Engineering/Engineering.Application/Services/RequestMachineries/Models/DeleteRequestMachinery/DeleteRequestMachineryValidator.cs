namespace Engineering.Application.Services.RequestMachineries.Models.DeleteRequestMachinery;

public class DeleteRequestMachineryValidator : AbstractValidator<DeleteRequestMachineryRequest>
{
    public DeleteRequestMachineryValidator()
    {
        RuleFor(oo => oo.RequestMachineryId).NotNull().WithError(RequestMachineryErrors.InValidRequestMachinery);
    }
}
