using Machinery = Engineering.Domain.Entities.Machineries.Machinery;

namespace Engineering.Application.Services.Machineries.Queries.GetMachineryById;

public record GetMachineryByIdQuery(
    long Id
    ) : IQuery<Machinery?>;