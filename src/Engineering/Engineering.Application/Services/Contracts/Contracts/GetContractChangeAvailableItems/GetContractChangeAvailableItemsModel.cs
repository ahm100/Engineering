namespace Engineering.Application.Services.Contracts.Contracts.GetContractChangeAvailableItems;

public class GetContractChangeAvailableItemsModel
{
    public bool IsOriginalContractItem { get; set; }
    public long? ContractTypeDetailId { get; set; }
    public long ContractTypeId { get; set; }
    public long SourceId { get; set; }
    public string SourceTitle { get; set; } = string.Empty;
    public string? SourceCode { get; set; }
    public string? SourceDescription { get; set; }
    public long? ProjectOperationId { get; set; }
    public long? ProjectOperationDetailId { get; set; }
    public decimal CurrentValue { get; set; }
    public long? UnitOfMeasurementId { get; set; }
    public decimal? UnitPrice { get; set; }
    public decimal? AvailableQuantity { get; set; }
}
