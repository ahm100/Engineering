using Engineering.Domain.Entities.Contracts.Enums;

namespace Engineering.Application.Services.Contracts.Contracts.ContractTypeDetails;

public record PriceIndexContractTypeDetailAdjustmentRequest(
    int BaseYear,
    ContractAdjustmentPeriod BasePeriod,
    long ReferenceId,
    long IndexId);
