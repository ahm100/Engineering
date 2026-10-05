namespace Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models.GetWithSelectedSkillId;

public class GetWithSelectedSkillIdResponseModel
{
    [JsonProperty("data")]
    public List<GetWithSelectedSkillIdUserModel>? Data { get; set; }

    [JsonProperty("rowCount")]
    public int RowCount { get; set; }
}
