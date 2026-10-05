
namespace Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models.GetFilteredThirdPartiesForSnap;

public record GetFilteredThirdPartiesForSnapRequest(
    List<string>? OrganizationCodes,
    List<string>? DefaultPhoneNumbers,
    string? FilterData,
    string[]? orderBy,
    int PageIndex,
    int PageSize
    );
