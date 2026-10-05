namespace Engineering.Application.Services.RequestMachineryManagements.Commands.UpdateRequestMachineryOnPaymentState;

public class UpdateRequestMachineryOnPaymentStateCommandValidator : AbstractValidator<UpdateRequestMachineryOnPaymentStateCommand>
{
    public UpdateRequestMachineryOnPaymentStateCommandValidator()
    {
        RuleFor(oo => oo.RequestMachineriesId).NotNull().NotEmpty().WithError(RequestMachineryInquiryErrors.InValidRequestMachinery);
        RuleFor(oo => oo.Status).IsInEnum().NotNull().WithError(RequestMachineryInquiryErrors.InValidStatus);
    }
}
