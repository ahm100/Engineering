using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.WebServices.MetaDataServices.Skills.Models.GetFilteredSkillByIds;

namespace Engineering.Application.WebServices.MetaDataServices.Skills.Queries.GetFilteredSkillByIds;

public class GetFilteredSkillByIdsQueryHandler : IQueryHandler<GetFilteredSkillByIdsQuery, DataResult<List<Skill>>>
{
    private readonly IMetaDataService _metaDataService;
    private readonly ILogger<GetFilteredSkillByIdsQueryHandler> _logger;

    public GetFilteredSkillByIdsQueryHandler(ILogger<GetFilteredSkillByIdsQueryHandler> logger, IMetaDataService repository)
    {
        _logger = logger;
        _metaDataService = repository;
    }

    public async Task<Result<DataResult<List<Skill>>?>> Handle(GetFilteredSkillByIdsQuery request, CT ct)
    {
        try
        {
            var result = await _metaDataService.GetFilteredSkillByIds(request.Adapt<GetFilteredSkillByIdsRequest>(), ct);

            return (result?.Value?.Data?.Any()) ?? false ?
                new DataResult<List<Skill>>
                {
                    Data = result.Value.Data,
                    RowCount = result.Value.RowCount
                } : Result.Failure<DataResult<List<Skill>>>(SharedErrors.ItemNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<Skill>>>(SharedErrors.UnknownError);
        }
    }
}
