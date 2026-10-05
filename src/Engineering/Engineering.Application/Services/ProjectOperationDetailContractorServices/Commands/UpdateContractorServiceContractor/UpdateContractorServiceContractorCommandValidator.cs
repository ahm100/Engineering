
namespace Engineering.Application.Services.ProjectOperationDetailContractorServices.Commands.UpdateContractorServiceContractor;

public class UpdateContractorServiceContractorCommandValidator : AbstractValidator<UpdateContractorServiceContractorCommand>
{
    public UpdateContractorServiceContractorCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().NotEmpty().WithError(ContractorServiceErrors.IdIsEmpty)
            .GreaterThan(0).WithError(GlobalErrors.IdMustGreaterZiro);

        RuleFor(oo => oo.ContractorId).NotNull().NotEmpty().WithError(ContractorServiceErrors.UnValidContactorId)
            .GreaterThan(0).WithError(ContractorServiceErrors.ContractorIdMustGreaterThanZero);
    }
}