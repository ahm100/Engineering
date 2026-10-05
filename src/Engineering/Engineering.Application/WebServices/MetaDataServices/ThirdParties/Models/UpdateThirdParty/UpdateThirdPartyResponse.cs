using Gita.Backend.Shared.Domain.Enums.Document;

namespace Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models.UpdateThirdParty;

public record UpdateThirdPartyResponse(
    long? Id,
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
    UpdateThirdPartyLegalModel? Legal,
    List<UpdateThirdPartyContactModel>? Contacts,
    List<UpdateThirdPartyAddressModel>? Addresses,
    List<UpdateThirdPartyDocumentModel>? Documents,
    List<UpdateThirdPartyBankAccountModel>? BankAccounts,
    List<UpdateThirdPartyInsuranceModel>? Insurances,
    List<UpdateThirdPartySkillModel>? Skills,
    List<UpdateThirdPartyCompanyModel>? Companies,
    List<UpdateThirdPartyLeaderModel>? Leaders,
    List<UpdateThirdPartyPresenterModel>? Presenters,
    List<UpdateThirdPartyActivityTypeModel>? ActivityTypes
);

public record UpdateThirdPartyLegalModel(
    string? LogoUrl,
    string? CompanyName,
    string? RegistrationNo,
    string? RegistrationDate,
    string? RegistrationLocation);

public record UpdateThirdPartySkillModel(long? SkillId);

public record UpdateThirdPartyCompanyModel(long? CompanyId, bool? IsActive);

public record UpdateThirdPartyLeaderModel(long? LeaderId);

public record UpdateThirdPartyActivityTypeModel(long? ActivityTypeId);

public record UpdateThirdPartyContactModel(string? ContactPoint, string? Description, long? ContactTypeId,
    bool? IsActive);

public record UpdateThirdPartyAddressModel(
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

public record UpdateThirdPartyBankAccountModel(
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

public record UpdateThirdPartyPresenterModel(long? PresenterId, string? Description);

public record UpdateThirdPartyDocumentModel(
    long? DocumentTitleId,
    string? Description,
    string? FileUrl,
    FileType? FileType,
    bool? IsActive);

public record UpdateThirdPartyInsuranceModel(
    string? Title,
    string? Description,
    long? InsuranceCompanyId,
    string? InsuranceNo,
    bool? IsActive);