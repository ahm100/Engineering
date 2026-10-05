using Engineering.Application.Services.EngineeringConfigs.Contracts.GetConfigHistoryByConfigId;

namespace Engineering.Application.Services.EngineeringConfigs.Queries.GetConfigHistoryByConfigId;

public record GetConfigHistoryByConfigIdQuery(
    long ConfigId,
    int PageIndex,
    int PageSize
    ) : IQuery<GetConfigHistoryByConfigIdResponse?>;