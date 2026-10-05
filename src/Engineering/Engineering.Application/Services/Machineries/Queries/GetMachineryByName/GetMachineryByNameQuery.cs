using Machinery = Engineering.Domain.Entities.Machineries.Machinery;

namespace Engineering.Application.Services.Machineries.Queries.GetMachineryByName;

public record GetMachineryByNameQuery(
    string MachineryName,
    long? CompanyId
    ) : IQuery<Machinery?>;