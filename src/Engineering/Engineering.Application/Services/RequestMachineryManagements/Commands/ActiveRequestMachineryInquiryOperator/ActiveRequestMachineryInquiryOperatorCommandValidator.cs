namespace Engineering.Application.Services.RequestMachineryManagements.Commands.ActiveRequestMachineryInquiryOperator;

public class ActiveRequestMachineryInquiryOperatorCommandValidator : AbstractValidator<ActiveRequestMachineryInquiryOperatorCommand>
{
    public ActiveRequestMachineryInquiryOperatorCommandValidator()
    {
        RuleFor(oo => oo.RequestMachineryId).NotNull().WithError(RequestMachineryInquiryOperatorErrors.InValidRequestMachinery);
        RuleFor(oo => oo.OperatorId).NotNull().WithError(RequestMachineryInquiryOperatorErrors.InValidOperatorId);
    }
}
