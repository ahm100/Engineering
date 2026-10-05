
namespace Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models.GetsThirdPartyById;

public record GetsThirdPartyByIdRequest(
    int PageIndex,
    int PageSize,
    List<long> Ids,
    bool IgnoreQuery
    );
