using SupervisorModel = Engineering.Application.WebServices.MetaDataServices.Supervisors.Models.Supervisor;

namespace Engineering.Application.WebServices.MetaDataServices.Supervisors.Models.GetSupervisorById;

public class GetSupervisorByIdResponse
{
    [JsonProperty("data")]
    public List<SupervisorModel>? Data { get; set; }

    [JsonProperty("rowCount")]
    public int RowCount { get; set; }
}
