namespace Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models.GetsTransportationThirdParty;

public record GetsTransportationThirdPartyRequest(
    List<long>? Ids,
    string? FilterData,
    int PageIndex,
    int PageSize,
    bool? IgnoreQuery);