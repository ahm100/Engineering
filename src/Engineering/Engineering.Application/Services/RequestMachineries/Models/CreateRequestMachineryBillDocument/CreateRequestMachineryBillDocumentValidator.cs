namespace Engineering.Application.Services.RequestMachineries.Models.CreateRequestMachineryBillDocument;

public class CreateRequestMachineryBillDocumentValidator : AbstractValidator<CreateRequestMachineryBillDocumentRequest>
{
    public CreateRequestMachineryBillDocumentValidator()
    {
        RuleFor(oo => oo.RequestMachineryId).NotNull().NotEmpty().WithError(RequestMachineryErrors.InValidRequestMachineryId)
            .GreaterThan(0).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}
