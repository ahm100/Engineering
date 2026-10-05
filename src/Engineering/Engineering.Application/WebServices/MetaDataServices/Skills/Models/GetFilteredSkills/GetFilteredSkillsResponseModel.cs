
namespace Engineering.Application.WebServices.MetaDataServices.Skills.Models.GetFilteredSkills;

public class GetFilteredSkillsResponseModel
{
    [JsonProperty("data")]
    public List<Skill>? Data { get; set; }

    [JsonProperty("rowCount")]
    public int RowCount { get; set; }
}
