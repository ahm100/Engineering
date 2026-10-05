using Engineering.Domain.Entities.Contracts.Enums;

namespace Engineering.Application.Services.Contracts.Contracts.ContractTypeDetails;

public record ContractTypeDetailAdjustmentRequest(
    ContractTypeDetailAdjustmentType Type,
    PriceIndexContractTypeDetailAdjustmentRequest? PriceIndex,
    CurrencyContractTypeDetailAdjustmentRequest? Currency,
    OtherContractTypeDetailAdjustmentRequest? Other);
