namespace Engineering.Application.Services.RequestMachineryManagements.Commands.InActiveRequestMachineryInquiryOperator;

public class InActiveRequestMachineryInquiryOperatorCommandValidator : AbstractValidator<InActiveRequestMachineryInquiryOperatorCommand>
{
    public InActiveRequestMachineryInquiryOperatorCommandValidator()
    {
        RuleFor(oo => oo.RequestMachineryId).NotNull().WithMessage(RequestMachineryInquiryOperatorErrors.InValidRequestMachinery);
        RuleFor(oo => oo.OperatorId).NotNull().WithMessage(RequestMachineryInquiryOperatorErrors.InValidOperatorId);
    }
}
