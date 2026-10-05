namespace Engineering.Application.Services.RequestMachineries.Commands.CreateRequestMachineryBillDocument;

public class CreateRequestMachineryBillDocumentCommandValidator : AbstractValidator<CreateRequestMachineryBillDocumentCommand>
{
    public CreateRequestMachineryBillDocumentCommandValidator()
    {
        RuleFor(oo => oo.RequestMachinery).NotNull().NotEmpty().WithError(RequestMachineryErrors.InValidRequestMachinery);
        RuleFor(oo => oo.Url).NotNull().NotEmpty().WithError(RequestMachineryErrors.InValidDocument);
    }
}
