namespace Engineering.Application.WebServices.MetaDataServices.ContractorEmployees.Models.GetById;

public record ContractorEmployeesByIdResponse
{
    [JsonProperty("value")]
    public ContractorEmployeesByIdModel? Value { get; set; }
}
