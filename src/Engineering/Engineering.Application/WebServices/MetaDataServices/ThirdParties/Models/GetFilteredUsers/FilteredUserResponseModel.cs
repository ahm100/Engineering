using Engineering.Application.WebServices.MetaDataServices.Employees.Models;

namespace Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models.GetFilteredUsers;

public record FilteredUserResponseModel(
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
    bool? IsActive,
    string? FirstName,
    string? LastName,
    string? Nickname,
    string? FatherName,
    string? IdentityNo,
    string? IdentitySerialNo,
    long? EconomicCode,
    List<ContactResponse>? Contacts
);
