using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.WebServices.MetaDataServices.CostGroups.Models.GetCostGroupById;
using CostGroupModel = Engineering.Application.WebServices.MetaDataServices.CostGroups.Models.CostGroup;

namespace Engineering.Application.WebServices.MetaDataServices.CostGroups.Queries.GetCostGroupById;

public class GetCostGroupByIdQueryHandler : IQueryHandler<GetCostGroupByIdQuery, CostGroupModel?>
{
    private readonly ILogger<GetCostGroupByIdQueryHandler> _logger;
    private readonly IMetaDataService _metaDataService;

    public GetCostGroupByIdQueryHandler(ILogger<GetCostGroupByIdQueryHandler> logger, IMetaDataService metaDataService)
    {
        _logger = logger;
        _metaDataService = metaDataService;
    }

    public async Task<Result<CostGroupModel?>> Handle(GetCostGroupByIdQuery request, CT ct)
    {
        try
        {
            var result = await _metaDataService.GetCostGroupById(request.Adapt<GetCostGroupByIdRequest>(), ct);

            return result?.Value ?? Result.Failure<CostGroupModel?>(SharedErrors.ProviderError);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<CostGroupModel?>(SharedErrors.UnknownError);
        }
    }
}
