using Gita.Backend.Shared.Domain.Enums.Document;

namespace Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models.CreateThirdParty;

public record CreateThirdPartyRequest(
    bool? IsIndividual,
    string FirstName,
    string LastName,
    string DefaultPhoneNo,
    string? DefaultEmailAdd,
    string? OrganizationCode,
    string? FatherName,
    string? NationalCode,
    int? Nationality,
    int? Citizenship,
    long? referrerOrganizationId,
    string? description,
    long? organizationId,
    string? IdentitySerialNo,
    string? IdentityNo,
    string? AvatarUrl,
    bool IsActive,
    long? UserId,
    DateTime? BirthDate,
    string? UniqueCode,
    long? EconomicCode,
    long? CountryId,
    string? Nickname,
    long? EmploymentTypeId,
    long? EmploymentStatusId,
    string? NationalCodeSerialNo,
    int? Gender,
    int? DutyStatus,
    int? MaritalStatus,
    string? FirstNameEn,
    string? LastNameEn,
    string? ExportationPlace,
    string? Religion,
    string? Sect,
    bool? HasWorkPermit,
    bool? AsSoldierInComplex,
    bool? HasVeteranStatus,
    bool? HasDisability,
    int? WorkPermitIssuer,
    DateTime? ValidityDateWorkPermit,
    string? BirthLocation,
    string? WorkCertificateNo,
    DateTime? IssuanceDate,
    CreateThirdPartyLegalRequest? Legal,
    List<CreateThirdPartyContactRequest>? Contacts,
    List<CreateThirdPartyAddressRequest>? Addresses,
    List<CreateThirdPartyDocumentRequest>? Documents,
    List<CreateThirdPartyBankAccountRequest>? BankAccounts,
    List<CreateThirdPartyInsuranceRequest>? Insurances,
    List<CreateThirdPartySkillRequest>? Skills,
    List<CreateThirdPartyCompanyRequest>? Companies,
    List<CreateThirdPartyLeaderRequest>? Leaders,
    List<CreateThirdPartyRelationRequest>? Relations,
    List<CreateThirdPartyPresenterRequest>? Presenters,
    List<CreateThirdPartyActivityTypeRequest>? ActivityTypes,
    List<CreateThirdPartyGroupRequest>? ThirdPartyGroups
    ) : IHttpRequest;

public record CreateThirdPartyLegalRequest(
    string? LogoUrl,
    string CompanyName,
    string RegistrationNo,
    string? RegistrationDate,
    string? RegistrationLocation);
public record CreateThirdPartySkillRequest(long SkillId, bool IsMain);
public record CreateThirdPartyCompanyRequest(long CompanyId, bool IsActive);
public record CreateThirdPartyLeaderRequest(long LeaderId) : IHttpRequest;
public record CreateThirdPartyActivityTypeRequest(long ActivityTypeId);
public record CreateThirdPartyContactRequest(string ContactPoint, string? Description, long ContactTypeId, bool IsActive) : IHttpRequest;

public record CreateThirdPartyAddressRequest(
    string Title,
    string AddressText,
    string? PostalCode,
    string? ApartmentNo,
    string? BuzzerNo,
    string? FloorNo,
    long CityId,
    double? Latitude,
    double? Longitude,
    bool IsActive,
    bool? IsDefault);

public record CreateThirdPartyBankAccountRequest(
    long? BranchId,
    long? BankAccountTypeId,
    string OwnerName,
    string AccountNo,
    string ShabaNo,
    string CartNo,
    string Description,
    string Title,
    bool IsDefault,
    bool IsActive);

public record CreateThirdPartyPresenterRequest(long PresenterId, string? Description);

public record CreateThirdPartyDocumentRequest(
    long DocumentTitleId,
    string Description,
    string FileUrl,
    FileType FileType,
    bool IsActive);

public record CreateThirdPartyInsuranceRequest(
    string Title,
    string Description,
    long InsuranceCompanyId,
    string? InsuranceNo,
    bool IsActive);

public record CreateThirdPartyRelationRequest(long RelationId);
public record CreateThirdPartyGroupRequest(long GroupId);