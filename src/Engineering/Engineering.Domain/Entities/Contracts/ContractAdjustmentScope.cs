using Engineering.Domain.Entities.Contracts.Enums;
using ContractTypeKindEnum = Engineering.Domain.Entities.Contracts.Enums.ContractTypeKind;

namespace Engineering.Domain.Entities.Contracts;

public class ContractAdjustmentScope : AuditableEntity<ContractAdjustmentScope, long>
{
    public long ContractAdjustmentConfigurationId { get; private set; }
    public ContractAdjustmentConfiguration ContractAdjustmentConfiguration { get; private set; } = null!;
    public ContractAdjustmentScopeType ScopeType { get; private set; }
    public ContractTypeKindEnum? ContractTypeKind { get; private set; }
    public long? ContractTypeDetailId { get; private set; }

    private ContractAdjustmentScope()
    {
    }

    public ContractAdjustmentScope(
        ContractAdjustmentConfiguration configuration,
        ContractAdjustmentScopeType scopeType,
        ContractTypeKindEnum? contractTypeKind,
        long? contractTypeDetailId)
    {
        ContractAdjustmentConfiguration = Guard.Against.Null(
            configuration,
            nameof(configuration));
        ContractAdjustmentConfigurationId = configuration.Id;
        ScopeType = Guard.Against.EnumOutOfRange(scopeType, nameof(scopeType));

        var valid = ScopeType switch
        {
            ContractAdjustmentScopeType.WholeContract =>
                !contractTypeKind.HasValue && !contractTypeDetailId.HasValue,

            ContractAdjustmentScopeType.ContractTypeKind =>
                contractTypeKind.HasValue &&
                (contractTypeKind.Value is
                    ContractTypeKindEnum.Engineering or
                    ContractTypeKindEnum.Procurement or
                    ContractTypeKindEnum.Construction or
                    ContractTypeKindEnum.Services) &&
                !contractTypeDetailId.HasValue,

            ContractAdjustmentScopeType.ContractTypeDetail =>
                !contractTypeKind.HasValue &&
                contractTypeDetailId.HasValue &&
                contractTypeDetailId.Value > 0,

            _ => false
        };

        if (!valid)
            throw new InvalidOperationException(
                "Contract adjustment scope is invalid.");

        ContractTypeKind = contractTypeKind;
        ContractTypeDetailId = contractTypeDetailId;
    }
}
