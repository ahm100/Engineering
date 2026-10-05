using Engineering.Domain.Entities.Projects.Enums;

namespace Engineering.Application.Services.Projects.Models.ProjectStatusChanger;

public record ProjectStatusChangerRequest(
    long Id,
    ProjectStatus Status
     ) : IHttpRequest;
