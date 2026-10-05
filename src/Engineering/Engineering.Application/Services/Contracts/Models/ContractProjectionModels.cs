using Engineering.Domain.Entities.Contracts;
using Engineering.Domain.Entities.Contracts.Enums;

namespace Engineering.Application.Services.Contracts.Models;

public sealed record ContractStructureMutationProjection(
    List<long> TypeIdsToRemove,
    bool HasUnknownContractType,
    bool HasActiveDetailsConflict,
    bool ContainsCostPlus,
    bool RequiresContractCeilingAmount);

public sealed record ContractRegistrationMutationProjection(
    bool StructureChanged,
    bool HeaderChanged,
    bool DocumentsChanged,
    bool FinancialChanged,
    bool RequiresContractCeilingAmount,
    decimal ActiveFinancialChangeAmount);

public sealed record ContractChangeRequestProjection(
    bool HasDuplicateItems,
    List<long> ContractTypeDetailIds,
    List<long> SourceContractTypeIds,
    List<long> SourceIds,
    HashSet<(long ContractTypeId, long SourceId)> ReplacementSourceKeys);

public sealed record ContractTypeDetailPricingValues(
    long ContractId,
    PricingMethod PricingMethod,
    decimal Quantity,
    decimal? UnitPrice,
    decimal? FixedAmount,
    decimal? Duration);

public sealed record ContractLegalAmountValues(
    decimal InitialAmount,
    decimal FinalAmount);

public sealed record ContractTypeDetailSourceData(
    Dictionary<long, ContractTypeDetailSourceInfo> Products,
    Dictionary<long, ContractTypeDetailSourceInfo> Services,
    Dictionary<long, ContractTypeDetailSourceInfo> OperationDetails,
    Dictionary<long, string> MeasureUnits);

public sealed record ContractTypeDetailSourceInfo(
    string Title,
    string? Code);

public sealed record ContractSummaryChangeCreateExecutionResult(
    ContractChange Change,
    decimal FinalContractAmount,
    DateTime NewEndDate);

public sealed record ContractTypeSummary(
    long Id,
    long ContractId,
    ContractTypeKind Kind,
    PricingMethod PricingMethod,
    long ProjectId,
    long ContractPartyId);

public sealed record ContractChangeSnapshot(
    long Id,
    DateTime Date);

public sealed record ContractChangeMutationTarget(
    decimal FinancialChangeAmount,
    int? DurationChange,
    ContractChangeMode Mode);

public sealed record ContractChangeSourceInfo(
    string Title,
    string? Code,
    string? Description);

public sealed record ContractChangeSourceHistoryRow(
    long ContractTypeId,
    long ProjectOperationDetailId,
    long ContractChangeId,
    DateTime Date,
    decimal NewValue,
    long? UnitOfMeasurementId,
    decimal? UnitPrice);

public sealed record ContractChangeSourceData(
    Dictionary<long, ContractChangeSourceInfo> Products,
    Dictionary<long, ContractChangeSourceInfo> Services,
    Dictionary<long, ContractChangeSourceInfo> OperationDetails);

public sealed record CurrentOmittedItem(
    long? ContractTypeDetailId,
    long? ContractTypeId,
    ContractTypeKind Kind,
    long SourceId,
    PricingMethod PricingMethod,
    decimal NewValue,
    decimal? BaselineQuantity);

public sealed record PriorOmittedItem(
    long? ContractTypeDetailId,
    long? ContractTypeId,
    long? ProjectOperationDetailId,
    long ContractChangeId,
    DateTime Date,
    decimal NewValue);

public sealed class EffectiveContractTypeDetailAllocation
{
    public long ContractTypeDetailId { get; set; }
    public long? ConsumableVolumeProductId { get; set; }
    public long? ProjectOperationDetailId { get; set; }
    public long? ProjectOperationDetailContractorServiceId { get; set; }
    public decimal EffectiveQuantity { get; set; }
}

public sealed class EffectiveSourceBasedConstructionAllocation
{
    public long ContractId { get; set; }
    public long ContractTypeId { get; set; }
    public long ProjectOperationDetailId { get; set; }
    public decimal EffectiveQuantity { get; set; }
    public long? UnitOfMeasurementId { get; set; }
    public decimal? UnitPrice { get; set; }
}

public sealed record ContractChangeDetailContextRow(
    long Id,
    long ContractTypeId,
    ContractTypeKind Kind,
    PricingMethod PricingMethod,
    long SourceId,
    decimal Quantity,
    decimal? FixedAmount,
    long? UnitOfMeasurementId,
    decimal? UnitPrice);

public sealed record ContractChangeDetailItemRow(
    long ContractTypeDetailId,
    long ContractChangeId,
    DateTime Date,
    decimal NewValue);

public enum ContractChangeCapacityValidationStatus
{
    Valid,
    SourceMissing,
    ExceedsAvailableQuantity
}
