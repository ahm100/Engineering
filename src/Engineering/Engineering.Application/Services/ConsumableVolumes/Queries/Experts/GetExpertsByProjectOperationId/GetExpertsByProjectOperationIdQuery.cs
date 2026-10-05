using Engineering.Application.Services.ConsumableVolumes.Models.Experts.DataModels;

namespace Engineering.Application.Services.ConsumableVolumes.Queries.Experts.GetExpertsByProjectOperationId;

public record GetExpertsByProjectOperationIdQuery(
    long ProjectOperationId,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<ExpertsDataModel>>>;