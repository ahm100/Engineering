
namespace Engineering.Application.Services.ProjectOperationDetailContractorServices.Models.SetDetailContractorServiceToNew;

public class SetDetailContractorServiceToNewValidator : AbstractValidator<SetDetailContractorServiceToNewRequest>
{
    public SetDetailContractorServiceToNewValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(ContractorServiceErrors.IdIsEmpty);
    }
}
