
namespace Engineering.Application.Services.MachineriesGroups.Models.MachineriesGroupGroupDelete;

public record MachineriesGroupGroupDeleteRequest(
    List<long> Ids
    ) : IHttpRequest;
