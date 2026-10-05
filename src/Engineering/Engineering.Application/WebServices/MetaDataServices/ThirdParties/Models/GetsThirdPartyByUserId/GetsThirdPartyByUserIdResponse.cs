using ThirdPartyModel = Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models.ThirdParty;

namespace Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models.GetsThirdPartyByUserId;

public class GetsThirdPartyByUserIdResponse
{
    [JsonProperty("data")]
    public List<ThirdPartyModel>? Data { get; set; }

    [JsonProperty("rowCount")]
    public int RowCount { get; set; }
}
