
namespace Engineering.Application.WebServices.WarehouseServices.WarehouseAssetes.Queries.GetUnUsedWarehousesCategories;

public class GetUnUsedWarehousesCategoriesQuery : IQuery<DataResult<List<GetUnUsedWarehousesCategoriesModel>?>?>
{
    public List<long> WarehouseIds { get; set; }
    public List<long>? UsedCategoryIds { get; set; }
    public string? FilterData { get; set; }
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public GetUnUsedWarehousesCategoriesQuery(
        List<long> warehouseIds,
        List<long>? usedCategoryIds,
        string? filterData,
        int pageIndex,
        int pageSize
        )
    {
        WarehouseIds = warehouseIds;
        UsedCategoryIds = usedCategoryIds;
        FilterData = filterData;
        PageIndex = pageIndex;
        PageSize = pageSize;
    }
}

public class GetUnUsedWarehousesCategoriesResponse
{
    [JsonProperty("value")]
    public GetUnUsedWarehousesCategoriesResponseData? Value { get; set; }
}

public class GetUnUsedWarehousesCategoriesResponseData
{
    [JsonProperty("data")]
    public List<GetUnUsedWarehousesCategoriesModel>? Data { get; set; }

    [JsonProperty("rowCount")]
    public int RowCount { get; set; }
}

public class GetUnUsedWarehousesCategoriesModel
{
    public long CategoryId { get; set; }
    public string? CategoryName { get; set; } = string.Empty;
    public string? CategoryCode { get; set; } = string.Empty;
}
