using UserModel = Engineering.Application.IdentityServices.Users.Models.User;

namespace Engineering.Application.IdentityServices.Users.Models.GetUserById;

public class GetUserByIdResponse
{
    [JsonProperty("value")]
    public UserModel? Value { get; set; }
}
