namespace Engineering.Application.Services.Contractors.Queries.ContractorEmployees.GetActiveContractorEmployeesByEmployeeId;

public class GetActiveContractorEmployeesByEmployeeIdQueryValidator : AbstractValidator<GetActiveContractorEmployeesByEmployeeIdQuery>
{
    public GetActiveContractorEmployeesByEmployeeIdQueryValidator()
    {
        RuleFor(c => c.Id).NotNull().WithError(ContractorEmployeeErrors.InValidEmployeeId);
    }
}
