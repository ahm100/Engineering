using Gita.Backend.Shared.Domain.Enums.Document;

namespace Engineering.Application.WebServices.MetaDataServices.Employees.Models;

public record Employee(
    long Id,
    bool IsActive,
    bool IsIndividual,
    long? UserId,
    string FullName,
    string FirstName,
    string LastName,
    string? FatherName,
    string IdentityNo,
    string? IdentitySerialNo,
    string DefaultPhoneNo,
    string DefaultEmailAdd,
    string AvatarUrl,
    string NationalCode,
    string OrganizationCode,
    string UniqueCode,
    long? EconomicCode,
    LegalResponse? Legal,
    List<SkillResponse>? Skills,
    List<ContactResponse>? Contacts,
    List<BankAccountResponse> BankAccounts,
    List<InsuranceResponse> Insurances,
    List<DocumentResponse> Documents,
    List<LeaderResponse> Leaders,
    List<AddressResponse> Addresses
    );

public record LegalResponse(string LogoUrl, string CompanyName, string RegistrationNo,
    string RegistrationDate, string RegistrationLocation);

public record SkillResponse(string Name, string Code);

public record ContactResponse(long Id, string ContactPoint, string? Description, string ContactTypeTitle, long ContactTypeId,
    string ContactCategoryName);

public record BankAccountResponse(
    long Id,
    long BankId,
    string BranchName,
    string OwnerName,
    string AccountNo,
    string ShabaNo,
    string CartNo,
    string Description,
    long ThirdPartyId,
    bool IsActive,
    bool IsDefault,
    string Title);

public record InsuranceResponse(
    long Id,
    string Title,
    string InsuranceNo,
    string Description,
    string InsuranceCompanyName,
    long InsuranceCompanyId,
    long ThirdPartyId,
    bool IsActive);

public record DocumentResponse(
    long DocumentTitleId,
    string DocumentTitle,
    string? Description,
    string FileUrl,
    FileType FileType,
    string FileTypeName,
    bool IsActive) : IHttpRequest;

public record LeaderResponse(long Id, bool IsIndividual, long? UserId, string FullName);

public record AddressResponse(
    long Id,
    long CityId,
    string CityName,
    long ThirdPartyId,
    bool IsDefault,
    bool IsActive,
    string Title,
    string AddressText,
    string PostalCode,
    string ApartmentNo,
    string BuzzerNo,
    string FloorNo,
    double? Latitude,
    double? Longitude);