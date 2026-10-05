namespace Engineering.Application.WebServices.MetaDataServices.Contractors.Models.CreateContractor;

public record CreateContractorResponse
{
    [JsonProperty("value")]
    public Contractor? Value { get; set; }
}
