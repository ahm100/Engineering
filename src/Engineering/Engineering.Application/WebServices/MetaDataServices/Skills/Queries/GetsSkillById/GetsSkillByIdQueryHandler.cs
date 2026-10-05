using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.WebServices.MetaDataServices.Skills.Models.GetsSkillById;

namespace Engineering.Application.WebServices.MetaDataServices.Skills.Queries.GetsSkillById;

public class GetsSkillByIdQueryHandler : IQueryHandler<GetsSkillByIdQuery, DataResult<List<Skill>>>
{
    private readonly IMetaDataService _metaDataService;
    private readonly ILogger<GetsSkillByIdQueryHandler> _logger;

    public GetsSkillByIdQueryHandler(ILogger<GetsSkillByIdQueryHandler> logger, IMetaDataService repository)
    {
        _logger = logger;
        _metaDataService = repository;
    }

    public async Task<Result<DataResult<List<Skill>>?>> Handle(GetsSkillByIdQuery request, CT ct)
    {
        try
        {
            var result = await _metaDataService.GetsSkillById(request.Adapt<GetsSkillByIdRequest>(), ct);

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