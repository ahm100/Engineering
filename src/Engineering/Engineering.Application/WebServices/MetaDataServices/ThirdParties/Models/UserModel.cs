
namespace Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models;

public record UserModel(
    long Id,
    long? UserId,
    string? FullName,
    string? Nickname,
    string? OrganizationCode,
    string? DefaultPhoneNo,
    string? NationalCode,
    string? UniqueCode,
    string? AvatarUrl,
    bool? IsActive,
    Guid? PreferentialReferenceCode,
    List<UserSkill>? Skills
    );

public record UserSkill(
    long? Id,
    string? Name,
    string? Code
    );
