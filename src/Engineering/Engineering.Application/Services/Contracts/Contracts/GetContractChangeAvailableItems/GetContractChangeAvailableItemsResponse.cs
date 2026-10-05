using Engineering.Domain.Entities.Contracts.Enums;

namespace Engineering.Application.Services.Contracts.Contracts.GetContractChangeAvailableItems;

public record GetContractChangeAvailableItemsResponse(
    long ContractId,
    long ContractTypeId,
    ContractTypeKind Kind,
    PricingMethod PricingMethod,
    List<GetContractChangeAvailableItemsModel> Data,
    int RowCount);
