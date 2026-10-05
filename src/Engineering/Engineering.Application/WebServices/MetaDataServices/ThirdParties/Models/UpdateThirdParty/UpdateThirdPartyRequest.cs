namespace Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models.UpdateThirdParty;

public record UpdateThirdPartyRequest(
    long Id,
    bool? IsIndividual,
    string? FirstName,
    string? LastName,
    string? DefaultPhoneNo,
    string? DefaultEmailAdd,
    string? OrganizationCode,
    string? FatherName,
    string? NationalCode,
    string? IdentitySerialNo,
    string? IdentityNo,
    int? Nationality,
    int? Citizenship,
    long? ReferrerOrganizationId,
    long? OrganizationId,
    string? AvatarUrl,
    bool? IsActive,
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
    string? Description,
    UpdateThirdPartyLegalRequest? Legal,
    List<UpdateThirdPartyContactRequest>? Contacts,
    List<UpdateThirdPartyAddressRequest>? Addresses,
    List<UpdateThirdPartyDocumentRequest>? Documents,
    List<UpdateThirdPartyBankAccountRequest>? BankAccounts,
    List<UpdateThirdPartyInsuranceRequest>? Insurances,
    List<UpdateThirdPartySkillRequest>? Skills,
    List<UpdateThirdPartyCompanyRequest>? Companies,
    List<UpdateThirdPartyLeaderRequest>? Leaders,
    List<UpdateThirdPartyRelationRequest>? Relations,
    List<UpdateThirdPartyPresentersRequest>? Presenters,
    List<UpdateThirdPartyActivityTypesRequest>? ActivityTypes,
    List<UpdateThirdPartyGroupRequest>? ThirdPartyGroups
) : IHttpRequest;

public record UpdateThirdPartyLegalRequest(
    long? Id,
    string? LogoUrl,
    string? CompanyName,
    string? RegistrationNo,
    string? RegistrationDate,
    string? RegistrationLocation,
    bool? IsDeleted);

public record UpdateThirdPartyContactRequest(
    long? Id,
    string? ContactPoint,
    string? Description,
    long? ContactTypeId,
    bool? IsActive,
    bool? IsDeleted);

public record UpdateThirdPartyAddressRequest(
    long? Id,
    string? Title,
    string? AddressText,
    string? PostalCode,
    string? ApartmentNo,
    string? BuzzerNo,
    string? FloorNo,
    long? CityId,
    double? Latitude,
    double? Longitude,
    bool? IsActive,
    bool? IsDefault,
    bool? IsDeleted);

public record UpdateThirdPartyDocumentRequest(
    long? Id,
    long? DocumentTitleId,
    string? Description,
    string? FileUrl,
    int? FileType,
    bool? IsActive,
    bool? IsDeleted);

public record UpdateThirdPartyBankAccountRequest(
    long? Id,
    long? BranchId,
    long? BankAccountTypeId,
    string? OwnerName,
    string? AccountNo,
    string? ShabaNo,
    string? CartNo,
    string? Description,
    string? Title,
    bool? IsDefault,
    bool? IsActive,
    bool? IsDeleted);

public record UpdateThirdPartyPresentersRequest(long? PresenterId, string? Description);

public record UpdateThirdPartyInsuranceRequest(
    long? Id,
    string? Title,
    string? Description,
    long? InsuranceCompanyId,
    string? InsuranceNo,
    bool? IsActive,
    bool? IsDeleted);

public record UpdateThirdPartySkillRequest(long SkillId, bool IsMain);
public record UpdateThirdPartyCompanyRequest(long CompanyId, bool IsActive);
public record UpdateThirdPartyLeaderRequest(long? LeaderId);
public record UpdateThirdPartyActivityTypesRequest(long? ActivityTypeId);
public record UpdateThirdPartyRelationRequest(long? RelationId) : IHttpRequest;
public record UpdateThirdPartyGroupRequest(long GroupId) : IHttpRequest;