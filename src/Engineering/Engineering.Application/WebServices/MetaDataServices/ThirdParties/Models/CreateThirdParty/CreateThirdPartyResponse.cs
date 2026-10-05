using Gita.Backend.Shared.Domain.Enums.Document;

namespace Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models.CreateThirdParty;

public record CreateThirdPartyResponse(
    long? Id,
    bool? IsIndividual,
    string FirstName,
    string LastName,
    string DefaultPhoneNo,
    string? DefaultEmailAdd,
    string? OrganizationCode,
    string? FatherName,
    string NationalCode,
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
    string? FirstNameEn,
    string? LastNameEn,
    string? ExportationPlace,
    string? Religion,
    string? Sect,
    bool? HasWorkPermit,
    bool? AsSoldierInComplex,
    bool? HasVeteranStatus,
    bool? HasDisability,
    DateTime? ValidityDateWorkPermit,
    CreateThirdPartyLegalModel? Legal,
    List<CreateThirdPartyContactModel>? Contacts,
    List<CreateThirdPartyAddressModel>? Addresses,
    List<CreateThirdPartyDocumentModel>? Documents,
    List<CreateThirdPartyBankAccountModel>? BankAccounts,
    List<CreateThirdPartyInsuranceModel>? Insurances,
    List<CreateThirdPartySkillModel>? Skills,
    List<CreateThirdPartyCompanyModel>? Companies,
    List<CreateThirdPartyLeaderModel>? Leaders,
    List<CreateThirdPartyPresenterModel>? Presenters,
    List<CreateThirdPartyActivityTypeModel>? ActivityTypes
);

public record CreateThirdPartyLegalModel(
    string? LogoUrl,
    string? CompanyName,
    string? RegistrationNo,
    string? RegistrationDate,
    string? RegistrationLocation);

public record CreateThirdPartySkillModel(long? SkillId);

public record CreateThirdPartyCompanyModel(long? CompanyId, bool? IsActive);

public record CreateThirdPartyLeaderModel(long? LeaderId);

public record CreateThirdPartyActivityTypeModel(long? ActivityTypeId);

public record CreateThirdPartyContactModel(string? ContactPoint, string? Description, long? ContactTypeId,
    bool? IsActive);

public record CreateThirdPartyAddressModel(
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
    bool? IsDefault);

public record CreateThirdPartyBankAccountModel(
    long? BranchId,
    long? BankAccountTypeId,
    string? OwnerName,
    string? AccountNo,
    string? ShabaNo,
    string? CartNo,
    string? Description,
    string? Title,
    bool? IsDefault,
    bool? IsActive);

public record CreateThirdPartyPresenterModel(long? PresenterId, string? Description);

public record CreateThirdPartyDocumentModel(
    long? DocumentTitleId,
    string? Description,
    string? FileUrl,
    FileType? FileType,
    bool? IsActive);

public record CreateThirdPartyInsuranceModel(
    string? Title,
    string? Description,
    long? InsuranceCompanyId,
    string? InsuranceNo,
    bool? IsActive);