namespace Engineering.Application.WebServices.IdentityServices.Users.Models.GetUsersByActionId;

public record GetUsersByActionIdRequest(
    List<long> ActionIds);