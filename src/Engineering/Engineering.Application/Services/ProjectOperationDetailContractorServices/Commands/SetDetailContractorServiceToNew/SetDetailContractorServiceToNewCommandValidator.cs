
namespace Engineering.Application.Services.ProjectOperationDetailContractorServices.Commands.SetDetailContractorServiceToNew;

public class SetDetailContractorServiceToNewCommandValidator : AbstractValidator<SetDetailContractorServiceToNewCommand>
{
    public SetDetailContractorServiceToNewCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(ContractorServiceErrors.IdIsEmpty);
    }
}
