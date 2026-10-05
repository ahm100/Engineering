using UserModel = Engineering.Application.IdentityServices.Users.Models.User;

namespace Engineering.Application.IdentityServices.Users.Models.GetsUserById;

public class GetsUserByIdResponseModel
{
    [JsonProperty("data")]
    public List<UserModel>? Data { get; set; }

    [JsonProperty("rowCount")]
    public int RowCount { get; set; }
}
