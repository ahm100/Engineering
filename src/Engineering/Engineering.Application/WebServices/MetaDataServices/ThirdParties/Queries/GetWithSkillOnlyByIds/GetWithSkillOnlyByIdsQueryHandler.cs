using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models.GetWithSkillOnlyByIds;

namespace Engineering.Application.WebServices.MetaDataServices.ThirdParties.Queries.GetWithSkillOnlyByIds;

public class GetWithSkillOnlyByIdsQueryHandler : IQueryHandler<GetWithSkillOnlyByIdsQuery, DataResult<List<UserModel?>?>?>
{
    private readonly IMetaDataService _metaDataService;
    private readonly ILogger<GetWithSkillOnlyByIdsQueryHandler> _logger;

    public GetWithSkillOnlyByIdsQueryHandler(ILogger<GetWithSkillOnlyByIdsQueryHandler> logger, IMetaDataService repository)
    {
        _logger = logger;
        _metaDataService = repository;
    }

    public async Task<Result<DataResult<List<UserModel?>?>?>> Handle(GetWithSkillOnlyByIdsQuery request, CT ct)
    {
        try
        {
            var result = await _metaDataService.GetWithSkillOnlyByIds(request.Adapt<GetWithSkillOnlyByIdsRequest>(), ct);

            return (result?.Value?.Data?.Any() ?? false) ?
                new DataResult<List<UserModel?>?>
                {
                    Data = result.Value!.Data!,
                    RowCount = result.Value.RowCount
                } : Result.Failure<DataResult<List<UserModel?>?>>(SharedErrors.ItemNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<UserModel?>?>>(SharedErrors.UnknownError);
        }
    }
}
