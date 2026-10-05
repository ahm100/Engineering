using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models.GetsTransportationThirdParty;

namespace Engineering.Application.WebServices.MetaData.ThirdParties.Queries.GetsTransportationThirdParty;

public record GetsTransportationThirdPartyQuery(
    List<long>? Ids,
    string? FilterData,
    int PageIndex,
    int PageSize,
    bool? IgnoreQuery
    ) : IQuery<List<GetsTransportationThirdPartyModel>>;