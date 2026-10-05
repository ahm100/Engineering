namespace Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models.RemoveThirdParty;

public class RemoveThirdPartyResponse
{
    [JsonProperty("value")]
    public RemoveThirdPartyResponseModel? Value { get; set; }
}

public record RemoveThirdPartyResponseModel(
    long Id);