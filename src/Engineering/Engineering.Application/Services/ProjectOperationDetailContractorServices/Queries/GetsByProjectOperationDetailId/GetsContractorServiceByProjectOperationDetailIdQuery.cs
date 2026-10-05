using Engineering.Application.Services.ProjectOperationDetailContractorServices.Models.GetsByProjectOperationDetailId;

namespace Engineering.Application.Services.GetsByProjectOperationDetailId.Queries.GetsByProjectOperationDetailId;

public record GetsContractorServiceByProjectOperationDetailIdQuery(
    long ProjectOperationDetailId,
    string? ServiceInfoName,
    string? ServiceInfoCode,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<GetsContractorServiceByProjectOperationDetailIdModel>>>;