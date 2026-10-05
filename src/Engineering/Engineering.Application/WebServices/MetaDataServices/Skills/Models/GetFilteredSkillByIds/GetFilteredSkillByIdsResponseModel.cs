
namespace Engineering.Application.WebServices.MetaDataServices.Skills.Models.GetFilteredSkillByIds;

public class GetFilteredSkillByIdsResponseModel
{
    [JsonProperty("data")]
    public List<Skill>? Data { get; set; }

    [JsonProperty("rowCount")]
    public int RowCount { get; set; }
}
