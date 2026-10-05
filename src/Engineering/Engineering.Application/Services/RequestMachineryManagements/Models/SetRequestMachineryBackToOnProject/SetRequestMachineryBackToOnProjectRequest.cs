namespace Engineering.Application.Services.RequestMachineryManagements.Models.SetRequestMachineryBackToOnProject;

public record SetRequestMachineryBackToOnProjectRequest(List<long> Ids, string? Description) : IHttpRequest;
