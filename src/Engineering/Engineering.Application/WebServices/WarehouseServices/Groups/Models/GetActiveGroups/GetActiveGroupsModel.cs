namespace Engineering.Application.WebServices.WarehouseServices.Groups.Models.GetActiveGroups;

public class GetActiveGroupsModel
{
    [JsonProperty("data")]
    public List<ActiveGroupsModel>? Data { get; set; }

    [JsonProperty("rowCount")]
    public int RowCount { get; set; }
}
