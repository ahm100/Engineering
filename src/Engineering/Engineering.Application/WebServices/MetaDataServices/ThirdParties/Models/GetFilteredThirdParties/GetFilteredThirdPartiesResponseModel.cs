using Engineering.Application.WebServices.MetaDataServices.Contractors.Models;

namespace Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models.GetFilteredThirdParties;

public record GetFilteredThirdPartiesResponseModel
{
    [JsonProperty("data")]
    public List<Contractor>? Data { get; set; }

    [JsonProperty("rowCount")]
    public int RowCount { get; set; }
}
