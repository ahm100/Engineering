using RoleModel = Engineering.Application.IdentityServices.Roles.Models.Role;

namespace Engineering.Application.IdentityServices.Roles.Models.GetsRoleById;

public class GetsRoleByIdResponseModel
{
    [JsonProperty("data")]
    public List<RoleModel>? Data { get; set; }

    [JsonProperty("rowCount")]
    public int RowCount { get; set; }
}
