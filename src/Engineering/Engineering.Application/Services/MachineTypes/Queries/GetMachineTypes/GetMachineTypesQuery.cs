using Engineering.Domain.Entities.MachineTypes;

namespace Engineering.Application.Services.MachineTypes.Queries.GetMachineTypes;

public record GetMachineTypesQuery(
    List<long>? Ids,
    string? FilterData,
    bool? IsActive,
    long? CompanyId,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<MachineType>>>;
