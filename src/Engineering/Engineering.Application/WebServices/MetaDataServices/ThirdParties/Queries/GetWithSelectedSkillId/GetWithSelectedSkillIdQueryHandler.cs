using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models.GetWithSelectedSkillId;

namespace Engineering.Application.WebServices.MetaDataServices.ThirdParties.Queries.GetWithSelectedSkillId;

public class GetWithSelectedSkillIdQueryHandler : IQueryHandler<GetWithSelectedSkillIdQuery, DataResult<List<GetWithSelectedSkillIdUserModel?>?>?>
{
    private readonly IMetaDataService _metaDataService;
    private readonly ILogger<GetWithSelectedSkillIdQueryHandler> _logger;

    public GetWithSelectedSkillIdQueryHandler(ILogger<GetWithSelectedSkillIdQueryHandler> logger, IMetaDataService repository)
    {
        _logger = logger;
        _metaDataService = repository;
    }
    public async Task<Result<DataResult<List<GetWithSelectedSkillIdUserModel?>?>?>> Handle(GetWithSelectedSkillIdQuery request, CT ct)
    {
        try
        {
            var result = await _metaDataService.GetWithSelectedSkillId(request.Adapt<GetWithSelectedSkillIdRequest>(), ct);

            return (result?.Value?.Data?.Any() ?? false) ?
                new DataResult<List<GetWithSelectedSkillIdUserModel?>?>
                {
                    Data = result.Value!.Data!,
                    RowCount = result.Value.RowCount
                } : Result.Failure<DataResult<List<GetWithSelectedSkillIdUserModel?>?>>(SharedErrors.ItemNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<GetWithSelectedSkillIdUserModel?>?>>(SharedErrors.UnknownError);
        }
    }
}
