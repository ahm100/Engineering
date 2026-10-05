using ThirdPartyModel = Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models.ThirdParty;

namespace Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models.GetsThirdPartyById;

public class GetsThirdPartyByIdResponseModel
{
    [JsonProperty("data")]
    public List<ThirdPartyModel>? Data { get; set; }

    [JsonProperty("rowCount")]
    public int RowCount { get; set; }
}
