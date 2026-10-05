namespace Engineering.Application.Services.RequestMachineries.Commands.DeleteRequestMachineryDocuments;

public class DeleteRequestMachineryDocumentsCommandValidator : AbstractValidator<DeleteRequestMachineryDocumentsCommand>
{
    public DeleteRequestMachineryDocumentsCommandValidator()
    {
        RuleFor(x => x.Id).NotNull().NotEmpty().WithError(RequestMachineryErrors.InValidId)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}
