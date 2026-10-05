using BankModel = Engineering.Application.WebServices.MetaDataServices.Banks.Models.Bank;

namespace Engineering.Application.WebServices.MetaDataServices.Banks.Queries.GetBankById;

public record GetBankByIdQuery(
    long Id
    ) : IQuery<BankModel?>;
