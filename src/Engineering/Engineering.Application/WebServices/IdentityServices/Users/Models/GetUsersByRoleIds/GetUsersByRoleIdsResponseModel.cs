using UserModel = Engineering.Application.IdentityServices.Users.Models.User;

namespace Engineering.Application.IdentityServices.Users.Models.GetUsersByRoleIds;

public class GetUsersByRoleIdsResponseModel
{
    [JsonProperty("data")]
    public List<UserModel>? Data { get; set; }

    [JsonProperty("rowCount")]
    public int RowCount { get; set; }
}
