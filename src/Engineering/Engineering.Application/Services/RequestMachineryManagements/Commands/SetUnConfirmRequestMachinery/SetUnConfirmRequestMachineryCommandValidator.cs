namespace Engineering.Application.Services.RequestMachineryManagements.Commands.SetUnConfirmRequestMachinery;

public class SetUnConfirmRequestMachineryCommandValidator : AbstractValidator<SetUnConfirmRequestMachineryCommand>
{
    public SetUnConfirmRequestMachineryCommandValidator()
    {
        RuleFor(oo => oo.RequestMachinery).NotNull().NotEmpty().WithError(RequestMachineryInquiryErrors.InValidRequestMachinery);
    }
}
