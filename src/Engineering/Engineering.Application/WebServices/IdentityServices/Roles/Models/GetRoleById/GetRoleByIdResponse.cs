using RoleModel = Engineering.Application.IdentityServices.Roles.Models.Role;

namespace Engineering.Application.IdentityServices.Roles.Models.GetRoleById;

public class GetRoleByIdResponse
{
    [JsonProperty("value")]
    public RoleModel? Value { get; set; }
}
