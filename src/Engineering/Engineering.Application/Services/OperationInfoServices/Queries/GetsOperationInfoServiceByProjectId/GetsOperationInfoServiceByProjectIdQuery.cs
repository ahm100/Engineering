using Engineering.Application.Services.OperationInfoServices.Models.GetsOperationInfoServiceByProjectId;

namespace Engineering.Application.Services.OperationInfoServices.Queries.GetsOperationInfoServiceByProjectId;

public record GetsOperationInfoServiceByProjectIdQuery(
    long ProjectId,
    long? ExcludedServiceInfoId,
    string? ServiceInfoFilters,
    string? OperationInfoFilters,
    string? FilterData,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<GetsOperationInfoServiceByProjectIdModel>>>;