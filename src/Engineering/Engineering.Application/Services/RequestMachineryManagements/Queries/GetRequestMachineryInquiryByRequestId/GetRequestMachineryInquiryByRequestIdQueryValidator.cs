namespace Engineering.Application.Services.RequestMachineryManagements.Queries.GetRequestMachineryInquiryByRequestId;

public class GetRequestMachineryInquiryByRequestIdQueryValidator : AbstractValidator<GetRequestMachineryInquiryByRequestIdQuery>
{
    public GetRequestMachineryInquiryByRequestIdQueryValidator()
    {
        RuleFor(oo => oo.RequestMachineryId).NotNull().WithError(RequestMachineryInquiryErrors.InValidRequestMachinery);
    }
}
