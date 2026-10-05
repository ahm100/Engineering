using Engineering.Domain.Entities.OperationInfos.Enums;

namespace Engineering.Application.Services.ProjectOperationDetailSchedulings.Models.UpdateProjectOperationDetailDatesByDay;

public record UpdateProjectOperationDetailDatesByDayRequest(
    long CostCenterId,
    long ProjectId,
    List<long> OperationInfoIds,
    List<long> OperationLocationIds,
    int CountDay,
    OperationInfoDependencyType DependencyType
    ) : IHttpRequest;
