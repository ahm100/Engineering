using UserModel = Engineering.Application.IdentityServices.Users.Models.User;

namespace Engineering.Application.IdentityServices.Users.Queries.GetsUserById;

public record GetsUserByIdQuery(
    List<long> Ids
    ) : IQuery<DataResult<List<UserModel>>>;
