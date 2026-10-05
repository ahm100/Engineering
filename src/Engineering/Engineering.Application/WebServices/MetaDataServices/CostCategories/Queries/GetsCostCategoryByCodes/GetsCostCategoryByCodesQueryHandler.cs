using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.WebServices.MetaDataServices.CostCategories.Models.GetsCostCategoryByCodes;
using CostCategoryModel = Engineering.Application.WebServices.MetaDataServices.CostCategories.Models.CostCategory;

namespace Engineering.Application.WebServices.MetaDataServices.CostCategories.Queries.GetsCostCategoryByCodes;

public class GetsCostCategoryByCodesQueryHandler : IQueryHandler<GetsCostCategoryByCodesQuery, DataResult<List<CostCategoryModel>>>
{
    private readonly IMetaDataService _metaDataService;
    private readonly ILogger<GetsCostCategoryByCodesQueryHandler> _logger;

    public GetsCostCategoryByCodesQueryHandler(ILogger<GetsCostCategoryByCodesQueryHandler> logger, IMetaDataService repository)
    {
        _logger = logger;
        _metaDataService = repository;
    }

    public async Task<Result<DataResult<List<CostCategoryModel>>?>> Handle(GetsCostCategoryByCodesQuery request, CT ct)
    {
        try
        {
            var result = await _metaDataService.GetsCostCategoryByCodes(request.Adapt<GetsCostCategoryByCodesRequest>(), ct);

            return (result?.Value?.Data?.Any()) ?? false ?
                new DataResult<List<CostCategoryModel>>
                {
                    Data = result.Value.Data,
                    RowCount = result.Value.RowCount
                } : Result.Failure<DataResult<List<CostCategoryModel>>>(SharedErrors.ItemNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<CostCategoryModel>>>(SharedErrors.UnknownError);
        }
    }
}
