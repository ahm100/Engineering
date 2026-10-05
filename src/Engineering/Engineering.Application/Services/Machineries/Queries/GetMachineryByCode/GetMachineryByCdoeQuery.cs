using Machinery = Engineering.Domain.Entities.Machineries.Machinery;

namespace Engineering.Application.Services.Machineries.Queries.GetMachineryByCode;

public record GetMachineryByCodeQuery(
    string MachineryCode,
    long? CompanyId
    ) : IQuery<Machinery?>;