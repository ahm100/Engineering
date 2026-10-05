
namespace Engineering.Application.Services.ProjectTypes.Models.StateChangerProjectTypes;

public record StateChangerProjectTypesRequest(
    List<long> Ids,
    bool State
    ) : IHttpRequest;
