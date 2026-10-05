namespace Engineering.Application.Services.RequestMachineries.Models.CreateRequestMachineryDocument;

public class CreateRequestMachineryDocumentValidator : AbstractValidator<CreateRequestMachineryDocumentRequest>
{
    public CreateRequestMachineryDocumentValidator()
    {
        RuleFor(oo => oo.RequestMachineryId).NotNull().NotEmpty().WithError(RequestMachineryErrors.InValidRequestMachineryId)
            .GreaterThan(0).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}
