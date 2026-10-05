using Engineering.Domain.Entities.ProjectOperationDetails.Enums;

namespace Engineering.Application.Services.ProjectOperationDetails.Models.GroupProjectOperationDetailStatusChanger;

public record GroupProjectOperationDetailStatusChangerRequest(
    List<long> Ids,
    ProjectOperationDetailStatus Status,
    string? StatusDescription
     ) : IHttpRequest;
