namespace Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models.GetsTransportationThirdParty;

public class GetsTransportationThirdPartyResponseModel
{
    [JsonProperty("data")]
    public List<GetsTransportationThirdPartyModel>? Data { get; set; }

    [JsonProperty("rowCount")]
    public int RowCount { get; set; }
}