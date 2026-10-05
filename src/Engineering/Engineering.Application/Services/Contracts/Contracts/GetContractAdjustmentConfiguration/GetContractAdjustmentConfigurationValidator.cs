namespace Engineering.Application.Services.Contracts.Contracts.GetContractAdjustmentConfiguration;

public class GetContractAdjustmentConfigurationValidator
    : AbstractValidator<GetContractAdjustmentConfigurationRequest>
{
    public GetContractAdjustmentConfigurationValidator()
    {
        RuleFor(oo => oo.ContractId)
            .IsPositive(GlobalCmts.ContractId);
    }
}
