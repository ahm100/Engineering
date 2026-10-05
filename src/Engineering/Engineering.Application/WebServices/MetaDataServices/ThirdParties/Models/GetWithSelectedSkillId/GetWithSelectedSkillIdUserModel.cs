namespace Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models.GetWithSelectedSkillId;

public record GetWithSelectedSkillIdUserModel(
     long? Id,
     long? UserId,
     string? FullName,
     string? OrganizationCode,
     string? DefaultPhoneNo,
     string? NationalCode,
     string? UniqueCode,
     string? AvatarUrl
    );

