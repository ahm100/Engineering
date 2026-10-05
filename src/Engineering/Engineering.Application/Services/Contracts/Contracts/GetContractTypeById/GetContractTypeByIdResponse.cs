using Engineering.Domain.Entities.Contracts.Enums;

namespace Engineering.Application.Services.Contracts.Contracts.GetContractTypeById;

public class GetContractTypeByIdResponse
{
    public long Id { get; set; }

    public long ContractId { get; set; }

    public ContractTypeKind Kind { get; set; }
    public string KindTitle => Kind.GetEnumDescription();

    public PricingMethod PricingMethod { get; set; }
    public string PricingMethodTitle => PricingMethod.GetEnumDescription();
}
