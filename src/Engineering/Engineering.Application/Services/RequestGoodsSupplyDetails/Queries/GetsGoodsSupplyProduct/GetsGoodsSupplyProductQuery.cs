using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetsGoodsSupplyProduct;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Queries.GetsGoodsSupplyProduct;

public record GetsGoodsSupplyProductQuery(
    GetsGoodsSupplyProductRequest Request,
    List<long>? Ids,
    List<long>? ProductIds,
    List<long>? ManagerSelectedProductIds,
    long CompanyId,
    bool CheckThirdParty,
    bool IsExcel
    ) : IQuery<DataResult<List<GetsGoodsSupplyProductModel>>>;
