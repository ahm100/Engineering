namespace Engineering.Application.Services.FixAssetMachineries.Commands.DeleteFixAssetNotWorkDocument;

public class DeleteFixAssetNotWorkDocumentCommandValidator : AbstractValidator<DeleteFixAssetNotWorkDocumentCommand>
{
    public DeleteFixAssetNotWorkDocumentCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().NotEmpty().WithError(RequestMachineryErrors.InValidId);
    }
}
