
namespace Engineering.Application.IdentityServices.Users.Models.GetUsersByRoleIds;

public record GetUsersByRoleIdsRequest(
    List<long> RoleIds,
    List<long> UserIds,
    string? FilterData,
    int PageIndex,
    int PageSize
    );
