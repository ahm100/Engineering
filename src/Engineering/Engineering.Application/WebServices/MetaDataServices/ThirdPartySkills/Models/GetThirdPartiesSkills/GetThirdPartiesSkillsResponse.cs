namespace Engineering.Application.WebServices.MetaDataServices.ThirdPartySkills.Models.GetThirdPartiesSkills;

public class GetThirdPartiesSkillsResponse
{
    [JsonProperty("value")]
    public GetThirdPartiesSkillsResponseModel? Value { get; set; }
}

public class GetThirdPartiesSkillsResponseModel
{
    [JsonProperty("data")]
    public List<GetThirdPartiesSkillsModel>? Data { get; set; }

    [JsonProperty("rowCount")]
    public int RowCount { get; set; }
}

public class GetThirdPartiesSkillsModel
{
    public long Id { get; set; }
    public long ThirdPartyId { get; set; }
    public string ThirdParty { get; set; } = string.Empty;
    public long SkillId { get; set; }
    public string Skill { get; set; } = string.Empty;
}
