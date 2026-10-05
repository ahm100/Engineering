using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.WebServices.MetaDataServices.Skills.Models.GetSkillByCode;

namespace Engineering.Application.WebServices.MetaDataServices.Skills.Queries.GetSkillByCode;

public class GetSkillByCodeQueryHandler : IQueryHandler<GetSkillByCodeQuery, Skill?>
{
    private readonly ILogger<GetSkillByCodeQueryHandler> _logger;
    private readonly IMetaDataService _metaDataService;

    public GetSkillByCodeQueryHandler(ILogger<GetSkillByCodeQueryHandler> logger, IMetaDataService metaDataService)
    {
        _logger = logger;
        _metaDataService = metaDataService;
    }

    public async Task<Result<Skill?>> Handle(GetSkillByCodeQuery request, CT ct)
    {
        try
        {
            var result = await _metaDataService.GetSkillByCode(request.Adapt<GetSkillByCodeRequest>(), ct);

            return result?.Value ?? Result.Failure<Skill?>(SharedErrors.ProviderError);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<Skill?>(SharedErrors.UnknownError);
        }
    }
}