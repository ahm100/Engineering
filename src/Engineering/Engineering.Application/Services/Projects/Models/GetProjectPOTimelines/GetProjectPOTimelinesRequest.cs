namespace Engineering.Application.Services.Projects.Models.GetProjectPOTimelines;

public record GetProjectPOTimelinesRequest(
    long Id
     ) : IHttpRequest;