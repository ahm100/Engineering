
namespace Engineering.Application.WebServices.MetaDataServices.Skills.Models.GetSkillByCode;

public class GetSkillByCodeResponse
{
    [JsonProperty("value")]
    public Skill? Value { get; set; }
}
