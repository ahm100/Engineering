using Engineering.Domain.Entities.MachineTypes;

namespace Engineering.Application.Services.MachineTypes.Queries.GetsActiveMachineType;

public record GetsActiveMachineTypeQuery(
    string? FilterData,
    long? CompanyId,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<MachineType>>>;