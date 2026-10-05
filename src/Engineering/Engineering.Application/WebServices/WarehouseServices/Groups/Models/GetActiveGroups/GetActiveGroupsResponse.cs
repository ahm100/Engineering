namespace Engineering.Application.WebServices.WarehouseServices.Groups.Models.GetActiveGroups;

public class GetActiveGroupsResponse
{
    [JsonProperty("value")]
    public GetActiveGroupsModel? Value { get; set; }
}
