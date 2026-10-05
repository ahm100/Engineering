namespace Engineering.Application.Services.CostCenterTypes.Models.CostCenterTypeGroupDelete;

public record CostCenterTypeGroupDeleteRequest(
    List<long> Ids)
    : IHttpRequest;