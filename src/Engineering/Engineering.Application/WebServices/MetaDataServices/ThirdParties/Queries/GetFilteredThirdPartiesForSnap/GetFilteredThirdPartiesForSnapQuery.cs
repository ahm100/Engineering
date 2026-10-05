using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models.GetFilteredThirdPartiesForSnap;

namespace Engineering.Application.WebServices.MetaDataServices.ThirdParties.Queries.GetFilteredThirdPartiesForSnap;

public record GetFilteredThirdPartiesForSnapQuery(
    List<string>? OrganizationCodes,
    List<string>? DefaultPhoneNumbers,
    string? FilterData,
    string[]? orderBy,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<GetFilteredForSnapModel>>>;
