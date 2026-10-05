
using Machinery = Engineering.Domain.Entities.Machineries.Machinery;

namespace Engineering.Application.Services.Machineries.Queries.GetMachineries;

public record GetMachineriesQuery(
    List<long>? Ids,
    string? FilterData,
    long? CategoryId,
    string? MachineryName,
    string? MachineryCode,
    bool? IsActive,
    long? CompanyId,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<Machinery>>>;