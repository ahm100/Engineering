using Engineering.Application.Abstractions.Data;
using Engineering.Application.Abstractions.Data.EngineeringConfigs;
using Engineering.Application.Services.EngineeringConfigs.Contracts.GetCodingConfigByEngConfigId;
using Engineering.Domain.Errors.EngineeringConfigs;

namespace Engineering.Application.Services.EngineeringConfigs.Queries.GetCodingConfigByEngConfigId;

public class GetCodingConfigByEngConfigIdQueryHandler : IQueryHandler<GetCodingConfigByEngConfigIdQuery, GetCodingConfigByEngConfigIdResponse?>
{
    private readonly IEngineeringCodingConfigRepository _repository;
    private readonly IViewThirdPartyRepository _thirdPartyRepo;
    private readonly ILogger<GetCodingConfigByEngConfigIdQueryHandler> _logger;

    public GetCodingConfigByEngConfigIdQueryHandler(ILogger<GetCodingConfigByEngConfigIdQueryHandler> logger,
        IEngineeringCodingConfigRepository repository,
        IViewThirdPartyRepository thirdPartyRepo)
    {
        _logger = logger;
        _repository = repository;
        _thirdPartyRepo = thirdPartyRepo;
    }

    public async Task<Result<GetCodingConfigByEngConfigIdResponse?>> Handle(GetCodingConfigByEngConfigIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetCodingConfigByEngConfigModel(request.ConfigId,
                request.PageIndex,
                request.PageSize, ct);

            if (result.Data is null || result.RowCount < 1)
                return Result.Failure<GetCodingConfigByEngConfigIdResponse>(EngineeringConfigErrors.ActiveConfigNotFound);

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

            return new GetCodingConfigByEngConfigIdResponse(result.Data, result.RowCount);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetCodingConfigByEngConfigIdResponse>(SharedErrors.UnknownError);
        }
    }
}