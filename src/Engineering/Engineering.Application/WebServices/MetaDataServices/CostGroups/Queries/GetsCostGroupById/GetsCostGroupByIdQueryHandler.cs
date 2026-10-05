using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.WebServices.MetaDataServices.CostGroups.Models.GetsCostGroupById;
using CostGroupModel = Engineering.Application.WebServices.MetaDataServices.CostGroups.Models.CostGroup;

namespace Engineering.Application.WebServices.MetaDataServices.CostGroups.Queries.GetsCostGroupById;

public class GetsCostGroupByIdQueryHandler : IQueryHandler<GetsCostGroupByIdQuery, DataResult<List<CostGroupModel>>>
{
    private readonly IMetaDataService _metaDataService;
    private readonly ILogger<GetsCostGroupByIdQueryHandler> _logger;

    public GetsCostGroupByIdQueryHandler(ILogger<GetsCostGroupByIdQueryHandler> logger, IMetaDataService repository)
    {
        _logger = logger;
        _metaDataService = repository;
    }

    public async Task<Result<DataResult<List<CostGroupModel>>?>> Handle(GetsCostGroupByIdQuery request, CT ct)
    {
        try
        {
            var result = await _metaDataService.GetsCostGroupById(request.Adapt<GetsCostGroupByIdRequest>(), ct);

            return (result?.Value?.Data?.Any()) ?? false ?
                new DataResult<List<CostGroupModel>>
                {
                    Data = result.Value.Data,
                    RowCount = result.Value.RowCount
                } : Result.Failure<DataResult<List<CostGroupModel>>>(SharedErrors.ItemNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<CostGroupModel>>>(SharedErrors.UnknownError);
        }
    }
}
