
namespace Engineering.Application.Services.ProjectOperationDetailContractorServices.Models.SetContractorServiceToContract;

public class SetContractorServiceToContractValidator : AbstractValidator<SetContractorServiceToContractRequest>
{
    public SetContractorServiceToContractValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(ContractorServiceErrors.IdIsEmpty);
    }
}
