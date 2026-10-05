namespace Engineering.Application.Services.Contracts.Contracts.GetContractFinancialInformation;

public class GetContractFinancialInformationValidator
    : AbstractValidator<GetContractFinancialInformationRequest>
{
    public GetContractFinancialInformationValidator()
    {
        RuleFor(oo => oo.ContractId)
            .IsPositive(GlobalCmts.ContractId);
    }
}
