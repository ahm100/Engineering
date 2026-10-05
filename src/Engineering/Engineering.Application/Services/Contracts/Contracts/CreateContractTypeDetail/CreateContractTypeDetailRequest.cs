namespace Engineering.Application.Services.Contracts.Contracts.CreateContractTypeDetail;

using Engineering.Application.Services.Contracts.Contracts.ContractTypeDetails;
using Engineering.Domain.Entities.Contracts.Enums;

public record CreateContractTypeDetailRequest(
    long ContractId,
    long ContractTypeId,
    long SourceId,
    decimal Quantity,
    long? UnitOfMeasurementId,
    decimal? UnitPrice,
    decimal? FixedAmount,
    string? TechnicalSpecifications,
    string? ExpectedDeliverables,
    decimal? Duration,
    ContractDurationUnit? DurationUnit,
    bool? IsSubjectToAdjustment,
    ContractTypeDetailAdjustmentRequest? Adjustment) : IHttpRequest;
