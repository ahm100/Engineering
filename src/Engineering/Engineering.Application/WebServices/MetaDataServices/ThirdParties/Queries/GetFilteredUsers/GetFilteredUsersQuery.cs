using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models.GetFilteredUsers;

namespace Engineering.Application.WebServices.MetaDataServices.ThirdParties.Queries.GetFilteredUsers;

public record GetFilteredUsersQuery(
 List<long> UserId,
 string? FilterData,
 bool? IsActive,
 string? NationalCode,
 string? DefaultPhoneNo,
 int PageIndex,
 int PageSize) : IQuery<DataResult<List<FilteredUserResponseModel>>>;
