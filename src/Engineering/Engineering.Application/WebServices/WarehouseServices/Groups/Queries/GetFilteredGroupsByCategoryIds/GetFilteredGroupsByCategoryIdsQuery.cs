using Engineering.Application.WebServices.WarehouseServices.Groups.Models.GetFilteredGroupsByCategoryIds;

namespace Engineering.Application.WebServices.WarehouseServices.Groups.Queries.GetFilteredGroupsByCategoryIds;

public record GetFilteredGroupsByCategoryIdsQuery(List<long> CategoryIds,
                                                  List<long>? WareHouseIds,
                                                  List<long>? GroupIds,
                                                  int PageIndex,
                                                  int PageSize,
                                                  string? Code,
                                                  string? Title,
                                                  string? FilterData,
                                                  string? ProductFilterData,
                                                  bool? IsSalable,
                                                  bool? IsWastage) : IQuery<DataResult<List<FilteredGroupsModel>>>;