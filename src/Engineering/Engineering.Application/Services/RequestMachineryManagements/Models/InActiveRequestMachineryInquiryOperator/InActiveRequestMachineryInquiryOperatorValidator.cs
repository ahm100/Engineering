namespace Engineering.Application.Services.RequestMachineryManagements.Models.InActiveRequestMachineryInquiryOperator;

public class InActiveRequestMachineryInquiryOperatorValidator : AbstractValidator<InActiveRequestMachineryInquiryOperatorRequest>
{
    public InActiveRequestMachineryInquiryOperatorValidator()
    {
        RuleFor(oo => oo.RequestMachineryId).NotNull().WithError(RequestMachineryInquiryOperatorErrors.InValidRequestMachinery);
        RuleFor(oo => oo.OperatorId).NotNull().WithError(RequestMachineryInquiryOperatorErrors.InValidOperatorId);
    }
}
