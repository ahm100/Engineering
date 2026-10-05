using Engineering.Domain.Entities.Contracts.Enums;

namespace Engineering.Application.Services.Contracts.Contracts.ContractAdjustmentConfigurations;

public class ContractAdjustmentScopeValidator
    : AbstractValidator<ContractAdjustmentScopeRequest>
{
    private static readonly ContractTypeKind[] SupportedKinds =
    [
        ContractTypeKind.Engineering,
        ContractTypeKind.Procurement,
        ContractTypeKind.Construction,
        ContractTypeKind.Services
    ];

    public ContractAdjustmentScopeValidator()
    {
        RuleFor(oo => oo)
            .Must(HasValidShape)
            .WithError(ContractErrors.ContractAdjustmentConfigurationScopeInvalid);

        RuleFor(oo => oo.ContractTypeKinds)
            .HasNoDuplicates(oo => oo, ContractCmts.ContractTypeKindScope)
            .Must(oo => oo is null || oo.All(SupportedKinds.Contains))
            .WithError(ContractErrors.ContractAdjustmentConfigurationScopeInvalid);

        RuleFor(oo => oo.ContractTypeDetailIds)
            .HasNoDuplicates(oo => oo, ContractCmts.ContractTypeDetailScope)
            .Must(oo => oo is null || oo.All(id => id > 0))
            .WithError(ContractErrors.ContractAdjustmentConfigurationScopeInvalid);
    }

    private static bool HasValidShape(ContractAdjustmentScopeRequest request)
    {
        return !request.WholeContract &&
               (request.ContractTypeKinds?.Count ?? 0) == 1 &&
               (request.ContractTypeDetailIds?.Count ?? 0) == 0;
    }
}
