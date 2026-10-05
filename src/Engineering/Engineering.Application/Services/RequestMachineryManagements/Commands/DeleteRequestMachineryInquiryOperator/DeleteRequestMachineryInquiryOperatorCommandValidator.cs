namespace Engineering.Application.Services.RequestMachineryManagements.Commands.DeleteRequestMachineryInquiryOperator;

public class DeleteRequestMachineryInquiryOperatorCommandValidator : AbstractValidator<DeleteRequestMachineryInquiryOperatorCommand>
{
    public DeleteRequestMachineryInquiryOperatorCommandValidator()
    {
        RuleFor(oo => oo.RequestMachineryInquiryOperatorId).NotNull().WithError(RequestMachineryInquiryOperatorErrors.InValidRequestMachineryInquiryOperator);
    }
}
