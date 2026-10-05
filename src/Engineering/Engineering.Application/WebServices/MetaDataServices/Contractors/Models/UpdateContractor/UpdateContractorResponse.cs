namespace Engineering.Application.WebServices.MetaDataServices.Contractors.Models.UpdateContractor;

public record UpdateContractorResponse
{
    [JsonProperty("value")]
    public Contractor? Value { get; set; }
}
