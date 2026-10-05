using ContractorModel = Engineering.Application.WebServices.MetaDataServices.Contractors.Models.Contractor;

namespace Engineering.Application.WebServices.MetaDataServices.Contractors.Models.GetContractorById;

public class GetContractorByIdModel
{
    [JsonProperty("data")]
    public List<ContractorModel>? Data { get; set; }

    [JsonProperty("rowCount")]
    public int RowCount { get; set; }
}
