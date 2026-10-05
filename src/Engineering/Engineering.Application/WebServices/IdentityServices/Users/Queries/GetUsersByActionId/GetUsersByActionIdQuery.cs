using Engineering.Application.WebServices.IdentityServices.Users.Models.GetUsersByActionId;

namespace Engineering.Application.WebServices.IdentityServices.Users.Queries.GetUsersByActionId;

public record GetUsersByActionIdQuery(
    List<long> ActionIds) : IQuery<GetUsersByActionIdResponse?>;