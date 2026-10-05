
namespace Engineering.Application.Services.DetailContractorServices.Models.InactiveDetailContractorService;

public class InactiveDetailContractorServiceValidator : AbstractValidator<InactiveDetailContractorServiceRequest>
{
    public InactiveDetailContractorServiceValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(ContractorServiceErrors.IdIsEmpty);
    }
}
