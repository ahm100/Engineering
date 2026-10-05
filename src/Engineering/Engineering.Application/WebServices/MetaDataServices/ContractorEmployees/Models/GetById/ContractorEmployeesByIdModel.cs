namespace Engineering.Application.WebServices.MetaDataServices.ContractorEmployees.Models.GetById;

public record ContractorEmployeesByIdModel
{
    [JsonProperty("data")]
    public List<ContractorEmployee>? Data { get; set; }

    [JsonProperty("rowCount")]
    public int RowCount { get; set; }
}
