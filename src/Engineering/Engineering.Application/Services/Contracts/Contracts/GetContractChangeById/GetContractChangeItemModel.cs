using Engineering.Domain.Entities.Contracts.Enums;

namespace Engineering.Application.Services.Contracts.Contracts.GetContractChangeById;

public class GetContractChangeItemModel
{
    public long Id { get; set; }
    public bool IsOriginalContractItem { get; set; }
    public long? ContractTypeDetailId { get; set; }
    public long ContractTypeId { get; set; }
    public long SourceId { get; set; }
    public ContractTypeKind Kind { get; set; }
    public string KindTitle => Kind.GetEnumDescription();
    public PricingMethod PricingMethod { get; set; }
    public string PricingMethodTitle => PricingMethod.GetEnumDescription();
    public string SourceTitle { get; set; } = string.Empty;
    public string? SourceCode { get; set; }
    public string? SourceDescription { get; set; }
    public decimal PreviousValue { get; set; }
    public decimal NewValue { get; set; }
    public long? UnitOfMeasurementId { get; set; }
    public decimal? UnitPrice { get; set; }
    public decimal ChangeAmount { get; set; }
}
