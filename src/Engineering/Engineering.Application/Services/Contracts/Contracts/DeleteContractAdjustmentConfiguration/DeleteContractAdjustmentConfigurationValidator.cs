namespace Engineering.Application.Services.Contracts.Contracts.DeleteContractAdjustmentConfiguration;

public class DeleteContractAdjustmentConfigurationValidator
    : AbstractValidator<DeleteContractAdjustmentConfigurationRequest>
{
    public DeleteContractAdjustmentConfigurationValidator()
    {
        RuleFor(oo => oo.ContractId)
            .IsPositive(GlobalCmts.ContractId);

        RuleFor(oo => oo.Id)
            .IsPositive(GlobalCmts.Id);
    }
}
