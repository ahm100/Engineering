
namespace Engineering.Application.Services.ProjectTypes.Models.StateChangerProjectTypes;

public record InactivateProjectTypesRequest(
    List<long> Ids
    ) : IHttpRequest;
