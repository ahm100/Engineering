using Engineering.Domain.Entities.Projects.Enums;

namespace Engineering.Application.Services.Projects.Models.GroupProjectStatusChanger;

public record GroupProjectStatusChangerRequest(
    List<long> Ids,
    ProjectStatus Status
     ) : IHttpRequest;
