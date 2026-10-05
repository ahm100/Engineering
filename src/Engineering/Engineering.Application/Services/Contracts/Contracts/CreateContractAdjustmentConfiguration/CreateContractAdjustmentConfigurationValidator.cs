using Engineering.Application.Services.Contracts.Contracts.ContractAdjustmentConfigurations;
using Engineering.Application.Services.Contracts.Contracts.ContractTypeDetails;

namespace Engineering.Application.Services.Contracts.Contracts.CreateContractAdjustmentConfiguration;

public class CreateContractAdjustmentConfigurationValidator
    : AbstractValidator<CreateContractAdjustmentConfigurationRequest>
{
    public CreateContractAdjustmentConfigurationValidator()
    {
        RuleFor(oo => oo.ContractId)
            .IsPositive(GlobalCmts.ContractId);

        RuleFor(oo => oo.Method)
            .IsEnum(ContractCmts.AdjustmentMethod);

        RuleFor(oo => oo.Scope)
            .NotNull()
            .SetValidator(new ContractAdjustmentScopeValidator());

        RuleFor(oo => oo.Adjustment)
            .NotNull()
            .SetValidator(new ContractTypeDetailAdjustmentRequestValidator());
    }
}
