namespace Engineering.Application.Services.RequestContractors.Commands.DeleteRequestContractor;

public class DeleteRequestContractorCommandValidator : AbstractValidator<DeleteRequestContractorCommand>
{
    public DeleteRequestContractorCommandValidator()
    {
        RuleFor(oo => oo.RequestContractor).NotNull().WithError(RequestContractorErrors.InValidRequestContractor);
    }
}
