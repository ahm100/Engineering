namespace Engineering.Application.ContractorServices.Models.CreateContractors;

public class CreateContractorsValidator : AbstractValidator<CreateContractorsRequest>
{
    public CreateContractorsValidator()
    {
        RuleFor(c => c.ContractorId).NotNull().WithError(ContractorServicesErrors.ContractorIdIsEmpty);
    }
}