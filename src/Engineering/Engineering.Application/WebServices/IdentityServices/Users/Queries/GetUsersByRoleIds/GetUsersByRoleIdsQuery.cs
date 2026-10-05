using UserModel = Engineering.Application.IdentityServices.Users.Models.User;

namespace Engineering.Application.IdentityServices.Users.Queries.GetUsersByRoleIds;

public record GetUsersByRoleIdsQuery(
    List<long> RoleIds,
    List<long> UserIds,
    string? FilterData,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<UserModel>>>;
