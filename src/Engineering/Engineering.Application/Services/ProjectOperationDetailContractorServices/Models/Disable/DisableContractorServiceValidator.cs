
namespace Engineering.Application.Services.ProjectOperationDetailContractorServices.Models.DisableContractorService;

public class DisableContractorServiceValidator : AbstractValidator<DisableContractorServiceRequest>
{
    public DisableContractorServiceValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(ContractorServiceErrors.IdIsEmpty);
    }
}
