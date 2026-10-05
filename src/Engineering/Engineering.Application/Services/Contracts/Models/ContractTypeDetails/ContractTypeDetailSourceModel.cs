namespace Engineering.Application.Services.Contracts.Models.ContractTypeDetails;

public record ContractTypeDetailSourceModel(
    long SourceId,
    decimal AvailableQuantity,
    long? UnitOfMeasurementId,
    long? ContractorId,
    decimal? EstimatedUnitPrice);
