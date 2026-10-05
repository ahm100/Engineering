namespace Engineering.Application.Services.RequestMachineryManagements.Commands.DeleteRequestMachineryInquiries;

public class DeleteRequestMachineryInquiriesCommandValidator : AbstractValidator<DeleteRequestMachineryInquiriesCommand>
{
    public DeleteRequestMachineryInquiriesCommandValidator()
    {
        RuleFor(oo => oo.RequestMachinery).NotNull().NotEmpty().WithError(RequestMachineryInquiryErrors.InValidRequestMachinery);
    }
}
