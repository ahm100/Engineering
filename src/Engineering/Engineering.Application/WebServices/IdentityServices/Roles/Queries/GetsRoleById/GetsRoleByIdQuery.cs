using RoleModel = Engineering.Application.IdentityServices.Roles.Models.Role;

namespace Engineering.Application.IdentityServices.Roles.Queries.GetsRoleById;

public record GetsRoleByIdQuery(
    List<long> Ids
    ) : IQuery<DataResult<List<RoleModel>>>;
