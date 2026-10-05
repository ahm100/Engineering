using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.WebServices.MetaDataServices.CostCategories.Models.GetCostCategoryById;
using CostCategoryModel = Engineering.Application.WebServices.MetaDataServices.CostCategories.Models.CostCategory;

namespace Engineering.Application.WebServices.MetaDataServices.CostCategories.Queries.GetCostCategoryById;

public class GetCostCategoryByIdQueryHandler : IQueryHandler<GetCostCategoryByIdQuery, CostCategoryModel?>
{
    private readonly ILogger<GetCostCategoryByIdQueryHandler> _logger;
    private readonly IMetaDataService _metaDataService;

    public GetCostCategoryByIdQueryHandler(ILogger<GetCostCategoryByIdQueryHandler> logger, IMetaDataService metaDataService)
    {
        _logger = logger;
        _metaDataService = metaDataService;
    }

    public async Task<Result<CostCategoryModel?>> Handle(GetCostCategoryByIdQuery request, CT ct)
    {
        try
        {
            var result = await _metaDataService.GetCostCategoryById(request.Adapt<GetCostCategoryByIdRequest>(), ct);

            return result?.Value ?? Result.Failure<CostCategoryModel?>(SharedErrors.ProviderError);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<CostCategoryModel?>(SharedErrors.UnknownError);
        }
    }
}
