namespace Engineering.Application.WebServices.MetaDataServices.GetMachineryOperators.Models.GetMachineryOperators;

public class GetMachineryOperatorsModel
{
    [JsonProperty("data")]
    public List<OfficerModel>? Data { get; set; }

    [JsonProperty("rowCount")]
    public int RowCount { get; set; }
}
