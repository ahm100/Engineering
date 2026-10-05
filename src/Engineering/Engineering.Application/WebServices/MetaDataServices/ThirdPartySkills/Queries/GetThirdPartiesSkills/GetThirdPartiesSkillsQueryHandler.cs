using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.WebServices.MetaDataServices.ThirdPartySkills.Models.GetThirdPartiesSkills;

namespace Engineering.Application.WebServices.MetaDataServices.ThirdPartySkills.Queries.GetThirdPartiesSkills;

public class GetThirdPartiesSkillsQueryHandler : IQueryHandler<GetThirdPartiesSkillsQuery, DataResult<List<GetThirdPartiesSkillsModel?>?>?>
{
    private readonly IMetaDataService _metaDataService;
    private readonly ILogger<GetThirdPartiesSkillsQueryHandler> _logger;

    public GetThirdPartiesSkillsQueryHandler(ILogger<GetThirdPartiesSkillsQueryHandler> logger, IMetaDataService repository)
    {
        _logger = logger;
        _metaDataService = repository;
    }
    public async Task<Result<DataResult<List<GetThirdPartiesSkillsModel?>?>?>> Handle(GetThirdPartiesSkillsQuery request, CT ct)
    {
        try
        {
            var result = await _metaDataService.GetThirdPartiesSkills(request.Adapt<GetThirdPartiesSkillsRequest>(), ct);

            return (result.Value?.Data?.Any() ?? false) ?
                new DataResult<List<GetThirdPartiesSkillsModel?>?>
                {
                    Data = result.Value!.Data!,
                    RowCount = result.Value.RowCount
                } : Result.Failure<DataResult<List<GetThirdPartiesSkillsModel?>?>>(SharedErrors.ItemNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<GetThirdPartiesSkillsModel?>?>>(SharedErrors.UnknownError);
        }
    }
}
