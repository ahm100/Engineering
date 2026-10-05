
namespace Engineering.Application.WebServices.MetaDataServices.Supervisors.Models;

public record Supervisor(
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
