namespace Engineering.Application.Services.Contractors.Queries.ContractorEmployees.GetContractorEmployeesByContractorId;

public class GetContractorEmployeesByContractorIdQueryValidator : AbstractValidator<GetContractorEmployeesByContractorIdQuery>
{
    public GetContractorEmployeesByContractorIdQueryValidator()
    {
        RuleForEach(c => c.Ids).NotEmpty().WithError(ContractorEmployeeErrors.InValidContractorId);
    }
}
