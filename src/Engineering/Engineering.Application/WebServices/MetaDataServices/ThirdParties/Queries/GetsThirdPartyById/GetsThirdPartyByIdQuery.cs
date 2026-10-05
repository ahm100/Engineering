using ThirdPartyModel = Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models.ThirdParty;

namespace Engineering.Application.WebServices.MetaDataServices.ThirdParties.Queries.GetsThirdPartyById;

public record GetsThirdPartyByIdQuery(
    int PageIndex,
    int PageSize,
    List<long> Ids,
    bool IgnoreQuery
    ) : IQuery<DataResult<List<ThirdPartyModel?>?>?>;
