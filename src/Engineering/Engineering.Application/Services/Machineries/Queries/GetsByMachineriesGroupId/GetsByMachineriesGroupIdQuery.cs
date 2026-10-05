using Engineering.Domain.Entities.Machineries;


namespace Engineering.Application.Services.Machineries.Queries.GetsByMachineriesGroupId;

public record GetsByMachineriesGroupIdQuery(
    long MachineriesGroupId,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<Machinery>>>;