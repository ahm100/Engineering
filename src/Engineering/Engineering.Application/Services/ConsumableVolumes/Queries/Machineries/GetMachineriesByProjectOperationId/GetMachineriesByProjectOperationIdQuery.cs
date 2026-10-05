using Engineering.Application.Services.ConsumableVolumes.Models.Machineries.DataModels;

namespace Engineering.Application.Services.ConsumableVolumes.Queries.Machineries.GetMachineriesByProjectOperationId;

public record GetMachineriesByProjectOperationIdQuery(
    long ProjectOperationId,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<MachineriesDataModel>>>;