
namespace Engineering.Application.Services.ProjectTypes.Models.StateChangerProjectTypes;

public record ActivateProjectTypesRequest(
    List<long> Ids
    ) : IHttpRequest;
