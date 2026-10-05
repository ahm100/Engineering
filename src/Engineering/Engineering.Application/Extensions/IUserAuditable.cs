using Gita.Backend.Shared.Application.WebServices.IdentityServices.Users.Models;
using Gita.Backend.Shared.Application.WebServices.IdentityServices.Users.Queries.GetsUserById;

namespace Engineering.Application.Extensions;

public interface IUserAuditable
{
    long CreatorId { get; }
    long? UpdaterId { get; }
    string? Creator { get; set; }
    string? Updater { get; set; }
}

public static class AuditableExtensions
{
    public static async Task SetFullName<T>(
        this List<T> values,
        ISender mediator,
        CancellationToken ct = default) where T : IUserAuditable
    {
        var userIds = values
            .SelectMany(x => new[] { x.CreatorId }
                .Concat(x.UpdaterId.HasValue ? new[] { x.UpdaterId.Value } : Enumerable.Empty<long>()))
            .Distinct()
            .ToList();

        var getUsers = await mediator.Send(new GetsUserByIdQuery(userIds), ct);
        var users = getUsers.Value?.Data ?? new List<User>();
        var userDict = users.ToDictionary(x => x.Id, x => x.FullName);

        foreach (var value in values)
        {
            value.Creator = userDict.GetNameOrDefaultDictionary(value.CreatorId);
            value.Updater = value.UpdaterId.HasValue ? userDict.GetNameOrDefaultDictionary(value.UpdaterId.Value) : null;
        }
    }

    public static string GetNameOrDefaultDictionary(this Dictionary<long, string> dict, long id)
    {
        return dict.TryGetValue(id, out var name) ? name : "";
    }

}