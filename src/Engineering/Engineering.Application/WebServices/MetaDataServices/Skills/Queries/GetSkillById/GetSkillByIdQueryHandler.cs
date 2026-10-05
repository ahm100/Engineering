using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.WebServices.MetaDataServices.Skills.Models.GetSkillById;

namespace Engineering.Application.WebServices.MetaDataServices.Skills.Queries.GetSkillById;

public class GetSkillByIdQueryHandler : IQueryHandler<GetSkillByIdQuery, Skill?>
{
    private readonly ILogger<GetSkillByIdQueryHandler> _logger;
    private readonly IMetaDataService _metaDataService;

    public GetSkillByIdQueryHandler(ILogger<GetSkillByIdQueryHandler> logger, IMetaDataService metaDataService)
    {
        _logger = logger;
        _metaDataService = metaDataService;
    }

    public async Task<Result<Skill?>> Handle(GetSkillByIdQuery request, CT ct)
    {
        try
        {
            var result = await _metaDataService.GetSkillById(request.Adapt<GetSkillByIdRequest>(), ct);

            return result?.Value ?? Result.Failure<Skill?>(SharedErrors.ProviderError);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<Skill?>(SharedErrors.UnknownError);
        }
    }
}