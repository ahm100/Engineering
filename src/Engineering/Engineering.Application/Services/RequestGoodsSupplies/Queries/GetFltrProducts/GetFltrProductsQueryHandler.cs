using Engineering.Application.Abstractions.Data.MetaEntities;
using Engineering.Application.Abstractions.Data.Projects;
using Engineering.Application.Services.RequestGoodsSupplies.Models.GetFltrProducts;

namespace Engineering.Application.Services.RequestGoodsSupplies.Queries.GetFltrProducts;

public class GetFltrProductsQueryHandler : IQueryHandler<GetFltrProductsQuery, GetFltrProductsResponse?>
{
    private readonly ILogger<GetFltrProductsQueryHandler> _logger;
    private readonly IProjectProductRepository _ppRepository;
    private readonly IViewProductRepository _productRepository;

    public GetFltrProductsQueryHandler(ILogger<GetFltrProductsQueryHandler> logger,
        IProjectProductRepository ppRepository,
        IViewProductRepository productRepository)
    {
        _logger = logger;
        _ppRepository = ppRepository;
        _productRepository = productRepository;
    }

    public async Task<Result<GetFltrProductsResponse?>> Handle(GetFltrProductsQuery request, CT ct)
    {
        try
        {
            var projectProducts = await _ppRepository.GetProductByProjectId(request.ProjectId, ct);

            if (projectProducts is not null && projectProducts.Count > 1)
            {
                var groupIds = projectProducts.NullListed(x => x.ProductGroupId);
                var catIds = projectProducts.NullListed(x => x.ProductCategoryId);
                var products = await _productRepository.GetFltrProducts(catIds, groupIds, request.FilterData, request.PageIndex, request.PageSize, ct);
                if (products.Data is null || products.RowCount < 1)
                    return Result.Failure<GetFltrProductsResponse?>(ProjectErrors.FltrProductNotFound);
                return new GetFltrProductsResponse(products.Data, products.RowCount);
            }

            else
            {
                var products = await _productRepository.GetFltrProducts(null, null, request.FilterData, request.PageIndex, request.PageSize, ct);
                if (products.Data is null || products.RowCount < 1)
                    return Result.Failure<GetFltrProductsResponse?>(ProjectErrors.FltrProductNotFound);
                return new GetFltrProductsResponse(products.Data, products.RowCount);
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<GetFltrProductsResponse?>(SharedErrors.UnknownError);
        }
    }
}