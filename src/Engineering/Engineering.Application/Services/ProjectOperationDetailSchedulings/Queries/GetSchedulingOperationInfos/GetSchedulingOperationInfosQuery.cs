using Engineering.Domain.Entities.OperationInfos;

namespace Engineering.Application.Services.ProjectOperationDetailSchedulings.Queries.GetSchedulingProjectOperations;

public record GetSchedulingOperationInfosQuery(
    long CostCenterId,
    long ProjectId,
    string? FilterData,
    List<long>? OperationLocationIds,
    List<long>? OperationInfoIds,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<OperationInfo>>>;
