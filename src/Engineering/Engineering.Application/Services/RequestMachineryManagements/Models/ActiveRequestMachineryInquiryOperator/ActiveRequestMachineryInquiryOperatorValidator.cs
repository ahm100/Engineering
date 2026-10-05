namespace Engineering.Application.Services.RequestMachineryManagements.Models.ActiveRequestMachineryInquiryOperator;

public class ActiveRequestMachineryInquiryOperatorValidator : AbstractValidator<ActiveRequestMachineryInquiryOperatorRequest>
{
    public ActiveRequestMachineryInquiryOperatorValidator()
    {
        RuleFor(oo => oo.RequestMachineryId).NotNull().WithMessage(RequestMachineryInquiryOperatorErrors.InValidRequestMachinery);
        RuleFor(oo => oo.OperatorId).NotNull().WithMessage(RequestMachineryInquiryOperatorErrors.InValidOperatorId);
    }
}
