namespace Engineering.Application.WebServices.IdentityServices.Users.Models.GetUsersByActionId;

public class GetUsersByActionIdResponse
{
    [JsonProperty("value")]
    public GetUsersByActionIdResponseModel? Value { get; set; }
}

public class GetUsersByActionIdResponseModel
{
    [JsonProperty("data")]
    public List<GetUsersByActionIdModel>? Data { get; set; }

    [JsonProperty("rowCount")]
    public int RowCount { get; set; }
}

public class GetUsersByActionIdModel
{
    public long UserId { get; set; }
    public long ActionId { get; set; }
    public string ActionName { get; set; } = string.Empty;
}