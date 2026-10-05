namespace Engineering.Application.WebServices.MetaDataServices.Contractors.Models.GetContractorById;

public class GetContractorByIdResponse

{
    [JsonProperty("value")]
    public GetContractorByIdModel? Value { get; set; }
}
