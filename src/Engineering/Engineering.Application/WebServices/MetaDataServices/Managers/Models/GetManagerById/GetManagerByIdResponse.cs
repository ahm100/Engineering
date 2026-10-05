using ManagerModel = Engineering.Application.WebServices.MetaDataServices.Managers.Models.Manager;

namespace Engineering.Application.WebServices.MetaDataServices.Managers.Models.GetManagerById;

public class GetManagerByIdResponse
{
    [JsonProperty("data")]
    public List<ManagerModel>? Data { get; set; }

    [JsonProperty("rowCount")]
    public int RowCount { get; set; }
}
