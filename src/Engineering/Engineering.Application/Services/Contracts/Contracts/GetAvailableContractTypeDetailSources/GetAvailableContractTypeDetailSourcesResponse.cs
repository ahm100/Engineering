using Engineering.Domain.Entities.Contracts.Enums;

namespace Engineering.Application.Services.Contracts.Contracts.GetAvailableContractTypeDetailSources;

public record GetAvailableContractTypeDetailSourcesResponse(
    long ContractId,
    long ContractTypeId,
    ContractTypeKind Kind,
    PricingMethod PricingMethod,
    List<GetAvailableContractTypeDetailSourcesModel> Data,
    int RowCount)
{
    public string KindTitle => Kind.GetEnumDescription();

    public string PricingMethodTitle => PricingMethod.GetEnumDescription();
}