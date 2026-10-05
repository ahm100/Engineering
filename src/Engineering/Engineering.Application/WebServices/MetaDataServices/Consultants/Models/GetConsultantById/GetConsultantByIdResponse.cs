using ConsultantModel = Engineering.Application.WebServices.MetaDataServices.Consultants.Models.Consultant;

namespace Engineering.Application.WebServices.MetaDataServices.Consultants.Models.GetConsultantById;

public class GetConsultantByIdResponse
{
    [JsonProperty("data")]
    public List<ConsultantModel>? Data { get; set; }

    [JsonProperty("rowCount")]
    public int RowCount { get; set; }
}
