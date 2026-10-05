using Engineering.Application.Services.EngineeringConfigs.Contracts.GetCodingConfigById;

namespace Engineering.Application.Services.EngineeringConfigs.Queries.GetCodingConfigById;

public record GetCodingConfigByIdQuery(
    long Id
    ) : IQuery<GetCodingConfigByIdResponse?>;