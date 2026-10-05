namespace Engineering.Application.Services.RequestMachineryManagements.Models.SetRequestMachineryInquiryOperator;

public class SetRequestMachineryInquiryOperatorValidator : AbstractValidator<SetRequestMachineryInquiryOperatorRequest>
{
    public SetRequestMachineryInquiryOperatorValidator()
    {
        RuleFor(oo => oo.RequestMachineryId).NotNull().WithError(RequestMachineryInquiryOperatorErrors.InValidRequestMachinery);
        RuleFor(oo => oo.OperatorAppoinmentId).NotNull().WithError(RequestMachineryInquiryOperatorErrors.InValidOperatorId);
    }
}
