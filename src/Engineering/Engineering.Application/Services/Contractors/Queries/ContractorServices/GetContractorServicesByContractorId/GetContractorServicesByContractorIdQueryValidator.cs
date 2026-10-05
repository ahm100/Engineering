namespace Engineering.Application.Services.Contractors.Queries.ContractorServices.GetContractorServicesByContractorId;

public class GetContractorServicesByContractorIdQueryValidator : AbstractValidator<GetContractorServicesByContractorIdQuery>
{
    public GetContractorServicesByContractorIdQueryValidator()
    {
        RuleFor(c => c.ContractorId).NotNull().WithError(ContractorServicesErrors.ContractorIdIsEmpty);
    }
}
