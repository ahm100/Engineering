namespace Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models.GetFilteredByIds;

public record FilteredUserModel(
     long Id,
     string? FirstName,
     string? LastName,
     string? Nickname,
     string? OrganizationCode,
     string? IdentityNo,
     string? DefaultPhoneNo,
     bool? IsActive
    );

