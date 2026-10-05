
namespace Engineering.Application.Services.DetailContractorServices.Models.ActiveDetailContractorService;

public class ActiveDetailContractorServiceValidator : AbstractValidator<ActiveDetailContractorServiceRequest>
{
    public ActiveDetailContractorServiceValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(ContractorServiceErrors.IdIsEmpty);
    }
}
