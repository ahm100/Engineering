
namespace Engineering.Application.WebServices.MetaDataServices.Employers.Models;

public record Employer(
    long Id,
    bool? IsIndividual,
    long? UserId,
    string? FullName,
    string? DefaultPhoneNo,
    string? DefaultEmailAdd,
    string? AvatarUrl,
    string? NationalCode,
    string? OrganizationCode,
    string? UniqueCode,
    Legal? Legal
    );
