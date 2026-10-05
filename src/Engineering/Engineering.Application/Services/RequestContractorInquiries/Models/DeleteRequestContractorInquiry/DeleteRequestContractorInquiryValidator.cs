namespace Engineering.Application.Services.RequestContractors.Models.DeleteRequestContractorInquiry;

public class DeleteRequestContractorInquiryValidator : AbstractValidator<DeleteRequestContractorInquiryRequest>
{
    public DeleteRequestContractorInquiryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().NotEmpty().WithError(RequestContractorErrors.InValidId)
            .GreaterThan(0).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}
