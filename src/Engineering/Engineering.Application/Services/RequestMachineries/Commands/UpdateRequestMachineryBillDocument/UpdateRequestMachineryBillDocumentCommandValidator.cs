namespace Engineering.Application.Services.RequestMachineries.Commands.UpdateRequestMachineryBillDocument;

public class UpdateRequestMachineryBillDocumentCommandValidator : AbstractValidator<UpdateRequestMachineryBillDocumentCommand>
{
    public UpdateRequestMachineryBillDocumentCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().NotEmpty().WithError(RequestMachineryErrors.InValidId);
        RuleFor(oo => oo.Url).NotNull().NotEmpty().WithError(RequestMachineryErrors.InValidDocument);
    }
}
