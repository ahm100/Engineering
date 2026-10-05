using Engineering.Application.Services.Contracts.Contracts.ContractAdjustmentConfigurations;
using Engineering.Application.Services.Contracts.Contracts.ContractTypeDetails;

namespace Engineering.Application.Services.Contracts.Contracts.UpdateContractAdjustmentConfiguration;

public class UpdateContractAdjustmentConfigurationValidator
    : AbstractValidator<UpdateContractAdjustmentConfigurationRequest>
{
    public UpdateContractAdjustmentConfigurationValidator()
    {
        RuleFor(oo => oo.ContractId)
            .IsPositive(GlobalCmts.ContractId);

        RuleFor(oo => oo.Id)
            .IsPositive(GlobalCmts.Id);

        RuleFor(oo => oo.Scope)
            .NotNull()
            .SetValidator(new ContractAdjustmentScopeValidator());

        RuleFor(oo => oo.Adjustment)
            .NotNull()
            .SetValidator(new ContractTypeDetailAdjustmentRequestValidator());
    }
}
