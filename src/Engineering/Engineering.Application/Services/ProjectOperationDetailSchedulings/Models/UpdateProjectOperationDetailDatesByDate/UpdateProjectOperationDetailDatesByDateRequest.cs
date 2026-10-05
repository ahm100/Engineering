namespace Engineering.Application.Services.ProjectOperationDetailSchedulings.Models.UpdateProjectOperationDetailDatesByDate;

public record UpdateProjectOperationDetailDatesByDateRequest(
    long CostCenterId,
    long ProjectId,
    List<long> OperationInfoIds,
    List<long> OperationLocationIds,
    DateTime StartDate,
    DateTime EndDate
    ) : IHttpRequest;
