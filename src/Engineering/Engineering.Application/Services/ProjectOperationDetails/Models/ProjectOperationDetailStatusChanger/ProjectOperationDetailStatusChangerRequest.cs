using Engineering.Domain.Entities.ProjectOperationDetails.Enums;

namespace Engineering.Application.Services.ProjectOperationDetails.Models.ProjectOperationDetailStatusChanger;

public record ProjectOperationDetailStatusChangerRequest(
    long Id,
    ProjectOperationDetailStatus Status,
    string? StatusDescription
     ) : IHttpRequest;
