namespace Engineering.Application.WebServices.WarehouseServices.Groups.Models.GetFilteredGroupsByCategoryIds;

public class GetFilteredGroupsByCategoryIdsModel
{
    [JsonProperty("data")]
    public List<FilteredGroupsModel>? Data { get; set; }

    [JsonProperty("rowCount")]
    public int RowCount { get; set; }
}
