namespace Engineering.Application.Services.RequestMachineryManagements.Commands.RemoveRequestContractorMachinery;

public class RemoveRequestContractorMachineryCommandValidator : AbstractValidator<RemoveRequestContractorMachineryCommand>
{
    public RemoveRequestContractorMachineryCommandValidator()
    {
        RuleFor(oo => oo.RequestMachinery).NotNull().NotEmpty().WithError(RequestMachineryInquiryErrors.InValidRequestMachinery);
    }
}
