namespace Engineering.Application.WebServices.WarehouseServices.Groups.Models.GetFilteredGroupsByCategoryIds;

public record GetFilteredGroupsByCategoryIdsRequest(List<long> CategoryIds,
                                                    List<long>? WareHouseIds,
                                                    List<long>? GroupIds,
                                                    int PageIndex,
                                                    int PageSize,
                                                    string? Code,
                                                    string? Title,
                                                    string? FilterData,
                                                    string? ProductFilterData,
                                                    bool? IsSalable,
                                                    bool? IsWastage);