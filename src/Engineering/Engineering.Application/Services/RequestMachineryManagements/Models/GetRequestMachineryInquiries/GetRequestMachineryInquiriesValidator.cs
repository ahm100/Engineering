namespace Engineering.Application.Services.RequestMachineryManagements.Models.GetRequestMachineryInquiries;

public class GetRequestMachineryInquiriesValidator : AbstractValidator<GetRequestMachineryInquiriesRequest>
{
    public GetRequestMachineryInquiriesValidator()
    {
        RuleFor(oo => oo.RequestMachineryId).NotNull().WithError(RequestMachineryErrors.InValidRequestMachinery);
    }
}
