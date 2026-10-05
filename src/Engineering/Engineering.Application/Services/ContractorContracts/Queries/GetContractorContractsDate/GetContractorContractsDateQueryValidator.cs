
namespace Engineering.Application.Services.ContractorContracts.Queries.GetContractorContractsDate;

public class GetContractorContractsDateQueryValidator : AbstractValidator<GetContractorContractsDateQuery>
{
    public GetContractorContractsDateQueryValidator()
    {
        RuleFor(c => c.ProjectId)
            .NotNull().WithError(ContractorContractErrors.InValidContractorContractId)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);

        RuleFor(c => c.ContractorId)
            .NotNull().WithError(ContractorContractErrors.InValidContractorContractId)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}
