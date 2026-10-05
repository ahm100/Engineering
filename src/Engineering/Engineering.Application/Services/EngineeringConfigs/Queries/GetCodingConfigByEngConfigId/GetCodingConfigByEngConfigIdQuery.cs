using Engineering.Application.Services.EngineeringConfigs.Contracts.GetCodingConfigByEngConfigId;

namespace Engineering.Application.Services.EngineeringConfigs.Queries.GetCodingConfigByEngConfigId;

public record GetCodingConfigByEngConfigIdQuery(
    long ConfigId,
    int PageIndex,
    int PageSize
    ) : IQuery<GetCodingConfigByEngConfigIdResponse?>;