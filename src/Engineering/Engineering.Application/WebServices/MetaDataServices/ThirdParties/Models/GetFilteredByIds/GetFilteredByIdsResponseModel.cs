namespace Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models.GetFilteredByIds;

public class GetFilteredByIdsResponseModel
{
    [JsonProperty("data")]
    public List<FilteredUserModel>? Data { get; set; }

    [JsonProperty("rowCount")]
    public int RowCount { get; set; }
}
