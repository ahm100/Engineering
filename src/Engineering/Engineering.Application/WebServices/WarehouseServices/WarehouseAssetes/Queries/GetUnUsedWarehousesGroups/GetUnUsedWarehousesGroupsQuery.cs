
namespace Engineering.Application.WebServices.WarehouseServices.WarehouseAssetes.Queries.GetUnUsedWarehousesGroups;

public class GetUnUsedWarehousesGroupsQuery : IQuery<DataResult<List<GetUnUsedWarehousesGroupsModel>?>?>
{
    public List<long> WarehouseIds { get; set; }
    public List<long>? UsedGroupIds { get; set; }
    public string? FilterData { get; set; }
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public GetUnUsedWarehousesGroupsQuery(
        List<long> warehouseIds,
        List<long>? usedGroupIds,
        string? filterData,
        int pageIndex,
        int pageSize
        )
    {
        WarehouseIds = warehouseIds;
        UsedGroupIds = usedGroupIds;
        FilterData = filterData;
        PageIndex = pageIndex;
        PageSize = pageSize;
    }
}

public class GetUnUsedWarehousesGroupsResponse
{
    [JsonProperty("value")]
    public GetUnUsedWarehousesGroupsResponseData? Value { get; set; }
}

public class GetUnUsedWarehousesGroupsResponseData
{
    [JsonProperty("data")]
    public List<GetUnUsedWarehousesGroupsModel>? Data { get; set; }

    [JsonProperty("rowCount")]
    public int RowCount { get; set; }
}

public class GetUnUsedWarehousesGroupsModel
{
    public long GroupId { get; set; }
    public string? GroupName { get; set; } = string.Empty;
    public string? GroupCode { get; set; } = string.Empty;
}
