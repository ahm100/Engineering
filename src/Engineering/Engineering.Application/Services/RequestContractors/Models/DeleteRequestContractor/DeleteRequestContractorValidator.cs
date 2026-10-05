namespace Engineering.Application.Services.RequestContractors.Models.DeleteRequestContractor;

public class DeleteRequestContractorValidator : AbstractValidator<DeleteRequestContractorRequest>
{
    public DeleteRequestContractorValidator()
    {
        RuleFor(oo => oo.Id).NotNull().NotEmpty().WithError(RequestContractorErrors.InValidId)
            .GreaterThan(0).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}
