using Engineering.Application.WebServices.WarehouseServices.Groups.Models.GetGroupsForGoodsProduct;

namespace Engineering.Application.WebServices.WarehouseServices.Groups.Queries.GetGroupsForGoodsProduct;

public record GetGroupsForGoodsProductQuery(List<long> CategoryIds,
                                                  List<long>? WareHouseIds,
                                                  int PageIndex,
                                                  int PageSize,
                                                  string? Code,
                                                  string? Title,
                                                  string? FilterData,
                                                  bool? IsSalable,
                                                  bool? IsWastage) : IQuery<DataResult<List<GetGroupsForGoodsProductModel>>>;