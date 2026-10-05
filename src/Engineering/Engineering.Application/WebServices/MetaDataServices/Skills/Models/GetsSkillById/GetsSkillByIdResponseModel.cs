
namespace Engineering.Application.WebServices.MetaDataServices.Skills.Models.GetsSkillById;

public class GetsSkillByIdResponseModel
{
    [JsonProperty("data")]
    public List<Skill>? Data { get; set; }

    [JsonProperty("rowCount")]
    public int RowCount { get; set; }
}
