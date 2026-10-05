using Engineering.Domain.Entities.EngineeringConfig;

namespace Engineering.Application.Services.EngineeringConfigs.Queries.GetActiveConfig;

public record GetActiveConfigQuery(
    ) : IQuery<EngineeringConfig>;