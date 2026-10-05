namespace Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models.GetWithSkillOnlyByIds;

public class GetWithSkillOnlyByIdsResponseModel
{
    [JsonProperty("data")]
    public List<UserModel>? Data { get; set; }

    [JsonProperty("rowCount")]
    public int RowCount { get; set; }
}
