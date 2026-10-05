
namespace Engineering.Application.WebServices.MetaDataServices.Directors.Models;

public record Director(
    long Id,
    bool IsIndividual,
    long UserId,
    string FullName,
    string DefaultPhoneNo,
    string DefaultEmailAdd,
    string AvatarUrl,
    string NationalCode,
    string OrganizationCode
    );
