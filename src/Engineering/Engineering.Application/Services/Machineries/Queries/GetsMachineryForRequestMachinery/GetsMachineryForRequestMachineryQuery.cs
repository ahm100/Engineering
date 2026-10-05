
using Machinery = Engineering.Domain.Entities.Machineries.Machinery;

namespace Engineering.Application.Services.Machineries.Queries.GetsMachineryForRequestMachinery;

public record GetsMachineryForRequestMachineryQuery(
    long? ProjectId,
    long? ProjectOperationId,
    long? ProjectOperationDetailId,
    long? MachineriesGroupId,
    string? FilterData,
    bool? IsActive,
    long? CompanyId,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<Machinery>>>;