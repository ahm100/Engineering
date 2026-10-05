
namespace Engineering.Application.WebServices.MetaDataServices.Consultants.Models;

public record Consultant(
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
