using ThirdPartyModel = Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models.ThirdParty;

namespace Engineering.Application.WebServices.MetaDataServices.ThirdParties.Queries.GetsThirdPartyBuyUserId;

public record GetsThirdPartyByUserIdQuery(
    List<long> Ids,
    string? FilterData,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<ThirdPartyModel>>>;
