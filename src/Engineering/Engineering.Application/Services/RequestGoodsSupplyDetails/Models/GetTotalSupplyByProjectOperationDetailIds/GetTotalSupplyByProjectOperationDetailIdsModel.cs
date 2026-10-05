namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetTotalSupplyByProjectOperationDetailIds;

public class GetTotalSupplyByProjectOperationDetailIdsModel
{
    public long ProductOperationDetailId { get; set; }
    public decimal TotalSupplyCount { get; set; }
    public decimal RequestedCount { get; set; }
}
