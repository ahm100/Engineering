
namespace Engineering.Application.WebServices.MetaDataServices.ContractorEmployees.Models;

public record ContractorEmployee(
    long? Id,
    bool? IsIndividual,
    long? UserId,
    string? FullName,
    string? DefaultPhoneNo,
    string? DefaultEmailAdd,
    string? AvatarUrl,
    string? NationalCode,
    string? OrganizationCode,
    string? UniqueCode,
    bool? IsActive,
    string? BirthDate,
    LegalResponse? Legal
    );

public record LegalResponse(
    string LogoUrl,
    string CompanyName,
    string RegistrationNo,
    string RegistrationDate,
    string RegistrationLocation
    );