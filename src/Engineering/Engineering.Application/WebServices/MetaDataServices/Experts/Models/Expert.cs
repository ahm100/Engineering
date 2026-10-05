
namespace Engineering.Application.WebServices.MetaDataServices.Experts.Models;

public record Expert(
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
