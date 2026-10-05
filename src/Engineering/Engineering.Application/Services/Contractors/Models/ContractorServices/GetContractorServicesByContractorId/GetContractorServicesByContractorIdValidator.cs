namespace Engineering.Application.Services.Contractors.Models.ContractorServices.GetContractorServicesByContractorId;

public class GetContractorServicesByContractorIdValidator : AbstractValidator<GetContractorServicesByContractorIdRequest>
{
    public GetContractorServicesByContractorIdValidator()
    {
        RuleFor(c => c.Id).NotNull().WithError(ContractorServicesErrors.ContractorIdIsEmpty);
    }
}
