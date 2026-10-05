namespace Engineering.Application.WebServices.WarehouseServices.Groups.Models.GetFilteredGroupsByIds;

public class GetFilterdGroupsByIdsModel
{
    [JsonProperty("data")]
    public List<FilteredGroup>? Data { get; set; }

    [JsonProperty("rowCount")]
    public int RowCount { get; set; }
}
