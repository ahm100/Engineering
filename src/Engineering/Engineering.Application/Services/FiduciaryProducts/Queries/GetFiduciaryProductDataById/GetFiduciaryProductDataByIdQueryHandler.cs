using Engineering.Application.Abstractions.Data.FiduciaryProducts;
using Engineering.Application.Services.FiduciaryProducts.Models.GetFiduciaryProductById;

namespace Engineering.Application.Services.FiduciaryProducts.Queries.GetFiduciaryProductDataById;

public class GetFiduciaryProductDataByIdQueryHandler : IQueryHandler<GetFiduciaryProductDataByIdQuery, GetFiduciaryProductByIdResponse>
{
    private readonly IFiduciaryProductRepository _repository;
    private readonly ILogger<GetFiduciaryProductDataByIdQueryHandler> _logger;

    public GetFiduciaryProductDataByIdQueryHandler(IFiduciaryProductRepository repository, ILogger<GetFiduciaryProductDataByIdQueryHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<GetFiduciaryProductByIdResponse?>> Handle(GetFiduciaryProductDataByIdQuery request, CT ct)
    {
        try
        {
            var entity = await _repository.GetDataById(request.FiduciaryProductId, ct);
            if (entity is null)
                return Result.Failure<GetFiduciaryProductByIdResponse>(FiduciaryProductErrors.FiduciaryProductWithIdNotFound);

            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetFiduciaryProductByIdResponse>(SharedErrors.UnknownError);
        }
    }
}
