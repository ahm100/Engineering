
namespace Engineering.Application.WebServices.MetaDataServices.Planers.Models;

public record Planer(
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
