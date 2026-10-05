using Engineering.Application.Services.EngineeringConfigs.Contracts.GetFltrCodingConfigs;

namespace Engineering.Application.Services.EngineeringConfigs.Queries.GetFltrCodingConfigs;

public record GetFltrCodingConfigsQuery(
    bool? IsActive,
    int PageIndex,
    int PageSize
     ) : IQuery<GetFltrCodingConfigsResponse?>;
