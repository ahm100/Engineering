using BankModel = Engineering.Application.WebServices.MetaDataServices.Banks.Models.Bank;

namespace Engineering.Application.WebServices.MetaDataServices.Banks.Models.GetsBankById;

public class GetsBankByIdResponseModel
{
    [JsonProperty("data")]
    public List<BankModel>? Data { get; set; }

    [JsonProperty("rowCount")]
    public int RowCount { get; set; }
}
