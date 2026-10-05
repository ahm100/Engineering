using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.WebServices.MetaDataServices.Skills.Models.GetFilteredSkills;

namespace Engineering.Application.WebServices.MetaDataServices.Skills.Queries.GetFilteredSkills;

public class GetFilteredSkillsQueryHandler : IQueryHandler<GetFilteredSkillsQuery, DataResult<List<Skill>>>
{
    private readonly IMetaDataService _metaDataService;
    private readonly ILogger<GetFilteredSkillsQueryHandler> _logger;

    public GetFilteredSkillsQueryHandler(ILogger<GetFilteredSkillsQueryHandler> logger, IMetaDataService repository)
    {
        _logger = logger;
        _metaDataService = repository;
    }

    public async Task<Result<DataResult<List<Skill>>?>> Handle(GetFilteredSkillsQuery request, CT ct)
    {
        try
        {
            var result = await _metaDataService.GetFilteredSkills(request.Adapt<GetFilteredSkillsRequest>(), ct);

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