namespace Engineering.Application.Services.Contracts.Contracts.UpdateContractTypeDetail;

using Engineering.Application.Services.Contracts.Contracts.ContractTypeDetails;
using Engineering.Domain.Entities.Contracts.Enums;

public record UpdateContractTypeDetailRequest(
    long ContractId,
    long ContractTypeId,
    long Id,
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
