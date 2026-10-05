using Engineering.Application.Abstractions.Data.ProjectOperationDetails.ConsumableVolume;
using Engineering.Application.Services.ConsumableVolumes.Models.Products.DataModels;

namespace Engineering.Application.Services.ConsumableVolumes.Queries.Products.GetProductsByProjectOperationId;

public class GetProductsByProjectOperationIdQueryHandler : IQueryHandler<GetProductsByProjectOperationIdQuery, DataResult<List<ProductsDataModel>>>
{
    private readonly IConsumableVolumeProductRepository _repository;
    private readonly ILogger<GetProductsByProjectOperationIdQueryHandler> _logger;

    public GetProductsByProjectOperationIdQueryHandler(ILogger<GetProductsByProjectOperationIdQueryHandler> logger, IConsumableVolumeProductRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<ProductsDataModel>>?>> Handle(GetProductsByProjectOperationIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsByProjectOperationId(request.ProjectOperationId, request.PageIndex, request.PageSize, ct);

            return result.Data.Any() ?
                new DataResult<List<ProductsDataModel>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<ProductsDataModel>>>(ProjectOperationErrors.ProjectChildNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<ProductsDataModel>>>(SharedErrors.UnknownError);
        }
    }
}
