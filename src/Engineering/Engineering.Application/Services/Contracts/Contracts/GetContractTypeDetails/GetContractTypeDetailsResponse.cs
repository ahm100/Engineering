using Engineering.Domain.Entities.Contracts.Enums;

namespace Engineering.Application.Services.Contracts.Contracts.GetContractTypeDetails;

public record GetContractTypeDetailsResponse(
    long ContractId,
    long ContractTypeId,
    ContractTypeKind Kind,
    PricingMethod PricingMethod,
    List<GetContractTypeDetailsModel> Items)
{
    public string KindTitle => Kind.GetEnumDescription();

    public string PricingMethodTitle => PricingMethod.GetEnumDescription();
}