using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models.GetThirdPartyById;

namespace Engineering.Application.WebServices.MetaDataServices.ThirdParties.Queries.GetThirdPartyById;

public record GetThirdPartyByIdQuery(
    long Id
    ) : IQuery<ThirdPartyUserModel?>;
