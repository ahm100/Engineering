using Engineering.Application.Services.ProjectServices.Models.GetsServiceInfoByProjectId;

namespace Engineering.Application.Services.ProjectServices.Queries.GetsServiceInfoByProjectId;

public record GetsServiceInfoByProjectIdQuery(
        long ProjectId,
        string? FilterData,
        int PageIndex,
        int PageSize
    ) : IQuery<DataResult<List<GetsServiceInfoByProjectIdModel>>>;