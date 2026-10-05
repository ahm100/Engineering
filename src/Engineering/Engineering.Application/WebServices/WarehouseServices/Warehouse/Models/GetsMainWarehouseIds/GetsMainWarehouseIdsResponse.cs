namespace Engineering.Application.WebServices.WarehouseServices.Groups.Models.GetsMainWarehouseIds;

public class GetsMainWarehouseIdsResponse
{
    [JsonProperty("value")]
    public GetsMainWarehouseIdsResponseModel? Value { get; set; }
}

public class GetsMainWarehouseIdsResponseModel
{
    [JsonProperty("Data")]
    public List<long>? Data { get; set; }
}

