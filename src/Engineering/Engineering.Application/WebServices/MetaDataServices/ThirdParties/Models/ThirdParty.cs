
using Engineering.Application.WebServices.MetaDataServices.Employers.Models;

namespace Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models;

public record ThirdParty(
    long Id,
    bool IsIndividual,
    long? UserId,
    string? FullName,
    string? DefaultPhoneNo,
    string? DefaultEmailAdd,
    string? AvatarUrl,
    string? NationalCode,
    string? OrganizationCode,
    Guid? PreferentialReferenceCode,
    Legal? Legal
    );
