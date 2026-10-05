using BankModel = Engineering.Application.WebServices.MetaDataServices.Banks.Models.Bank;

namespace Engineering.Application.WebServices.MetaDataServices.Banks.Models.GetBankById;

public class GetBankByIdResponse
{
    [JsonProperty("value")]
    public BankModel? Value { get; set; }
}
