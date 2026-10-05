using Engineering.Domain.Entities.Contracts.Enums;

namespace Engineering.Application.Services.Contracts.Models.ContractChanges;

public record ContractChangeSourceContextModel(
    ContractTypeKind Kind,
    long SourceId,
    decimal AvailableQuantity,
    long? UnitOfMeasurementId,
    decimal? EstimatedUnitPrice);
