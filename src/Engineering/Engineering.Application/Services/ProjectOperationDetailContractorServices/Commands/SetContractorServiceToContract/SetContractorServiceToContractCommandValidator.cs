
namespace Engineering.Application.Services.ProjectOperationDetailContractorServices.Commands.SetContractorServiceToContract;

public class SetContractorServiceToContractCommandValidator : AbstractValidator<SetContractorServiceToContractCommand>
{
    public SetContractorServiceToContractCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(ContractorServiceErrors.IdIsEmpty);
    }
}
