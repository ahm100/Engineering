
using Machinery = Engineering.Domain.Entities.Machineries.Machinery;

namespace Engineering.Application.Services.Machineries.Queries.GetActiveMachineries;

public record GetActiveMachineriesQuery(
    string? FilterData,
    long? CategoryId,
    string? MachineryCode,
    string? MachineryName,
    long? CompanyId,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<Machinery>>>;