namespace Engineering.Application.WebServices.MetaDataServices.MachineryInquiryOfficers.Models.GetMachineryInquiryOfficers;

public class GetMachineryInquiryOfficersModel
{
    [JsonProperty("data")]
    public List<Officer>? Data { get; set; }

    [JsonProperty("rowCount")]
    public int RowCount { get; set; }
}
