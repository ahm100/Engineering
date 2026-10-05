using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.WebServices.MetaDataServices.CostCategories.Models.GetsCostCategoryById;
using CostCategoryModel = Engineering.Application.WebServices.MetaDataServices.CostCategories.Models.CostCategory;

namespace Engineering.Application.WebServices.MetaDataServices.CostCategories.Queries.GetsCostCategoryById;

public class GetsCostCategoryByIdQueryHandler : IQueryHandler<GetsCostCategoryByIdQuery, DataResult<List<CostCategoryModel>>>
{
    private readonly IMetaDataService _metaDataService;
    private readonly ILogger<GetsCostCategoryByIdQueryHandler> _logger;

    public GetsCostCategoryByIdQueryHandler(ILogger<GetsCostCategoryByIdQueryHandler> logger, IMetaDataService repository)
    {
        _logger = logger;
        _metaDataService = repository;
    }

    public async Task<Result<DataResult<List<CostCategoryModel>>?>> Handle(GetsCostCategoryByIdQuery request, CT ct)
    {
        try
        {
            var result = await _metaDataService.GetsCostCategoryById(request.Adapt<GetsCostCategoryByIdRequest>(), ct);

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
