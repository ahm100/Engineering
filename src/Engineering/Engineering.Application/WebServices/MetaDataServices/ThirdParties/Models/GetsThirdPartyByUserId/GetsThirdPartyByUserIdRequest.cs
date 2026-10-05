
namespace Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models.GetsThirdPartyByUserId;

public record GetsThirdPartyByUserIdRequest(
    List<long> Ids,
    string? FilterData,
    int PageIndex,
    int PageSize
    );
