namespace Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models.GetFilteredUsers;

public record GetFilteredUsersRequest(
 List<long> UserId,
 string? FilterData,
 bool? IsActive,
 string? NationalCode,
 string? DefaultPhoneNo,
 int PageIndex,
 int PageSize
    );