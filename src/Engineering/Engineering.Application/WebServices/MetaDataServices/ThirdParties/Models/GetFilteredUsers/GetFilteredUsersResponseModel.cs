namespace Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models.GetFilteredUsers;

public record GetFilteredUsersResponseModel
{
    [JsonProperty("data")]
    public List<FilteredUserResponseModel>? Data { get; set; }

    [JsonProperty("rowCount")]
    public int RowCount { get; set; }
}
