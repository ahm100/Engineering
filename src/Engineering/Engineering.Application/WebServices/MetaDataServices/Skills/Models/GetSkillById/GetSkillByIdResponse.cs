namespace Engineering.Application.WebServices.MetaDataServices.Skills.Models.GetSkillById;

public class GetSkillByIdResponse
{
    [JsonProperty("value")]
    public Skill? Value { get; set; }
}
