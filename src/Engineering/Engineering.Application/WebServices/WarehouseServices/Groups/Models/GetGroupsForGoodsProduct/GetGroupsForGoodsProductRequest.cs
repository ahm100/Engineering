namespace Engineering.Application.WebServices.WarehouseServices.Groups.Models.GetGroupsForGoodsProduct;

public record GetGroupsForGoodsProductRequest(List<long> CategoryIds,
                                                    List<long>? WareHouseIds,
                                                    int PageIndex,
                                                    int PageSize,
                                                    string? Code,
                                                    string? Title,
                                                    string? FilterData,
                                                    bool? IsSalable,
                                                    bool? IsWastage);