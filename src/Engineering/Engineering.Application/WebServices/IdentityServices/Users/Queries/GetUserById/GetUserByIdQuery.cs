using UserModel = Engineering.Application.IdentityServices.Users.Models.User;

namespace Engineering.Application.IdentityServices.Users.Queries.GetUserById;

public record GetUserByIdQuery(
    long Id
    ) : IQuery<UserModel?>;
