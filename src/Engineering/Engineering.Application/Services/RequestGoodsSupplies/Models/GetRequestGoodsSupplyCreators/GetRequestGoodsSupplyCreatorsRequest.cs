namespace Engineering.Application.Services.RequestGoodsSupplies.Models.GetRequestGoodsSupplyCreators;

public record GetRequestGoodsSupplyCreatorsRequest(
    List<long>? CostCenterIds,
    List<long>? ProjectIds,
    List<long>? ProjectOperationIds,
    List<long>? ProjectOperationDetailIds,
    List<long>? RequestGoodsSupplyIds,
    string? FilterData,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
