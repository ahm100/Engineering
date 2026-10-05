using Engineering.Application.Abstractions.Data;
using Engineering.Application.Abstractions.Data.EngineeringConfigs;
using Engineering.Application.Services.EngineeringConfigs.Contracts.GetCodingConfigById;
using Engineering.Domain.Errors.EngineeringConfigs;

namespace Engineering.Application.Services.EngineeringConfigs.Queries.GetCodingConfigById;

public class GetCodingConfigByIdQueryHandler : IQueryHandler<GetCodingConfigByIdQuery, GetCodingConfigByIdResponse?>
{
    private readonly IEngineeringCodingConfigRepository _repository;
    private readonly IViewThirdPartyRepository _thirdPartyRepo;
    private readonly ILogger<GetCodingConfigByIdQueryHandler> _logger;

    public GetCodingConfigByIdQueryHandler(ILogger<GetCodingConfigByIdQueryHandler> logger,
        IEngineeringCodingConfigRepository repository,
        IViewThirdPartyRepository thirdPartyRepo)
    {
        _logger = logger;
        _repository = repository;
        _thirdPartyRepo = thirdPartyRepo;
    }

    public async Task<Result<GetCodingConfigByIdResponse?>> Handle(GetCodingConfigByIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetCodingConfigById(request.Id, ct);

            if (result is null)
                return Result.Failure<GetCodingConfigByIdResponse>(EngineeringConfigErrors.CodeConfigWithIdNotFound);

            var creatorId = result!.CreatorId;
            var creators = await _thirdPartyRepo.GetByUserIds([creatorId], ct);

            var creator = creators.FirstOrDefault();
            result.Creator = creator?.FirstName + " " + creator?.LastName;

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetCodingConfigByIdResponse>(SharedErrors.UnknownError);
        }
    }
}