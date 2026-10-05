using Engineering.Application.Abstractions.Data;
using Engineering.Application.Abstractions.Data.EngineeringConfigs;
using Engineering.Application.Services.EngineeringConfigs.Contracts.GetFltrCodingConfigs;
using Engineering.Domain.Errors.EngineeringConfigs;

namespace Engineering.Application.Services.EngineeringConfigs.Queries.GetFltrCodingConfigs;

public class GetFltrCodingConfigsQueryHandler : IQueryHandler<GetFltrCodingConfigsQuery, GetFltrCodingConfigsResponse?>
{
    private readonly IEngineeringCodingConfigRepository _repository;
    private readonly IViewThirdPartyRepository _thirdPartyRepo;
    private readonly ILogger<GetFltrCodingConfigsQueryHandler> _logger;

    public GetFltrCodingConfigsQueryHandler(ILogger<GetFltrCodingConfigsQueryHandler> logger,
        IEngineeringCodingConfigRepository repository,
        IViewThirdPartyRepository thirdPartyRepo)
    {
        _logger = logger;
        _repository = repository;
        _thirdPartyRepo = thirdPartyRepo;
    }

    public async Task<Result<GetFltrCodingConfigsResponse?>> Handle(GetFltrCodingConfigsQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetFltrCodingConfigs(request.IsActive,
                request.PageIndex,
                request.PageSize, ct);

            if (result.Data is null || result.RowCount < 1)
                return Result.Failure<GetFltrCodingConfigsResponse>(EngineeringConfigErrors.ActiveConfigNotFound);

            var creatorIds = result.Data!.Select(x => x.CreatorId);
            if (creatorIds is not null && creatorIds.Count() > 0)
            {
                var creators = await _thirdPartyRepo.GetByUserIds(creatorIds.Listed(x => x), ct);
                foreach (var item in result.Data!)
                {
                    var creator = creators.FirstOrDefault(x => x.UserId == item.CreatorId);
                    item.Creator = creator?.FirstName + " " + creator?.LastName;
                }
            }

            return new GetFltrCodingConfigsResponse(result.Data, result.RowCount);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetFltrCodingConfigsResponse>(SharedErrors.UnknownError);
        }
    }
}