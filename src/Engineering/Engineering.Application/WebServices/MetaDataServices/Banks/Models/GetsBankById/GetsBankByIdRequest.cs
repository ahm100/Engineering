
namespace Engineering.Application.WebServices.MetaDataServices.Banks.Models.GetsBankById;

public record GetsBankByIdRequest(
    int PageIndex,
    int PageSize,
    List<long> Ids,
    bool IgnoreQuery
    );
