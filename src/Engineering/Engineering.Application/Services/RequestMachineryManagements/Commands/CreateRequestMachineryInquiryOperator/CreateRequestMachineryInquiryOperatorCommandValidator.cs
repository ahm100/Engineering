namespace Engineering.Application.Services.RequestMachineryManagements.Commands.CreateRequestMachineryInquiryOperator;

public class CreateRequestMachineryInquiryOperatorCommandValidator : AbstractValidator<CreateRequestMachineryInquiryOperatorCommand>
{
    public CreateRequestMachineryInquiryOperatorCommandValidator()
    {
        RuleFor(oo => oo.OperatorAssinmentId).NotNull().WithError(RequestMachineryInquiryOperatorErrors.InValidOperatorId);
        RuleFor(oo => oo.RequestMachinery).NotEmpty().WithError(RequestMachineryInquiryOperatorErrors.InValidRequestMachinery);
    }
}
