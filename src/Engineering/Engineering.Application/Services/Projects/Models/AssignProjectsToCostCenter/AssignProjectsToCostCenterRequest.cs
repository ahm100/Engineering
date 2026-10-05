namespace Engineering.Application.Services.Projects.Models.AssignProjectsToCostCenter;

public record AssignProjectsToCostCenterRequest(
    long CostCenterId,
    List<long> ProjectIds
     ) : IHttpRequest;