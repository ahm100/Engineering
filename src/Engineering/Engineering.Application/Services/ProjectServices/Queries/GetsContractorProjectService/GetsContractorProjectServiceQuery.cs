
namespace Engineering.Application.Services.ProjectServices.Queries.GetsContractorProjectService;

public record GetsContractorProjectServiceQuery(
        long? ProjectId,
        long? ServiceInfoId,
        int PageIndex,
        int PageSize
    ) : IQuery<DataResult<List<long>>>;