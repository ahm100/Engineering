namespace Engineering.Application.Services.FixAssetMachineries.Commands.DeleteFixAssetMachineryDocument;

public class DeleteFixAssetMachineryDocumentCommandValidator : AbstractValidator<DeleteFixAssetMachineryDocumentCommand>
{
    public DeleteFixAssetMachineryDocumentCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().NotEmpty().WithError(RequestMachineryErrors.InValidId);
    }
}
