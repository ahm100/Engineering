using BankModel = Engineering.Application.WebServices.MetaDataServices.Banks.Models.Bank;

namespace Engineering.Application.WebServices.MetaDataServices.Banks.Queries.GetsBankById;

public record GetsBankByIdQuery(
    int PageIndex,
    int PageSize,
    List<long?> Ids,
    bool IgnoreQuery
    ) : IQuery<DataResult<List<BankModel>>>;
