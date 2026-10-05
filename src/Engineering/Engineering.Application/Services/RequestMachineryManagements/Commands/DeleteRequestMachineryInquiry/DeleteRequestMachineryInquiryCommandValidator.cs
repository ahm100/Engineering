namespace Engineering.Application.Services.RequestMachineryManagements.Commands.DeleteRequestMachineryInquiry;

public class DeleteRequestMachineryInquiryCommandValidator : AbstractValidator<DeleteRequestMachineryInquiryCommand>
{
    public DeleteRequestMachineryInquiryCommandValidator()
    {
        RuleFor(oo => oo.RequestMachineryInquiryId).NotNull().WithError(RequestMachineryInquiryErrors.InValidRequestMachineryInquiry);
    }
}
