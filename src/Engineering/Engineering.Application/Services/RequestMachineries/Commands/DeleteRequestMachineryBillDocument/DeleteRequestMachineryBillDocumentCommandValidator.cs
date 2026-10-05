namespace Engineering.Application.Services.RequestMachineries.Commands.DeleteRequestMachineryBillDocument;

public class DeleteRequestMachineryBillDocumentCommandValidator : AbstractValidator<DeleteRequestMachineryBillDocumentCommand>
{
    public DeleteRequestMachineryBillDocumentCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().NotEmpty().WithError(RequestMachineryErrors.InValidId);
    }
}
