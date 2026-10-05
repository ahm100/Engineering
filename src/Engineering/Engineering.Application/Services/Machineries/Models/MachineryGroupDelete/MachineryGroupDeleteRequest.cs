
namespace Engineering.Application.Services.Machineries.Models.MachineryGroupDelete;

public record MachineryGroupDeleteRequest(
    List<long> Ids
    ) : IHttpRequest;
