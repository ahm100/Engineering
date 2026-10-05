using RoleModel = Engineering.Application.IdentityServices.Roles.Models.Role;

namespace Engineering.Application.IdentityServices.Roles.Queries.GetRoleById;

public record GetRoleByIdQuery(
    long Id
    ) : IQuery<RoleModel?>;
