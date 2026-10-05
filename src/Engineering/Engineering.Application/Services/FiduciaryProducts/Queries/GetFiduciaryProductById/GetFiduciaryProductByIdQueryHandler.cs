using Engineering.Application.Abstractions.Data.FiduciaryProducts;
using Engineering.Domain.Entities.FiduciaryProducts;

namespace Engineering.Application.Services.FiduciaryProducts.Queries.GetFiduciaryProductById;

public class GetFiduciaryProductByIdQueryHandler : IQueryHandler<GetFiduciaryProductByIdQuery, FiduciaryProduct>
{
    private readonly IFiduciaryProductRepository _repository;
    private readonly ILogger<GetFiduciaryProductByIdQueryHandler> _logger;

    public GetFiduciaryProductByIdQueryHandler(IFiduciaryProductRepository repository, ILogger<GetFiduciaryProductByIdQueryHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<FiduciaryProduct?>> Handle(GetFiduciaryProductByIdQuery request, CT ct)
    {
        try
        {
            var entity = await _repository.GetByIdAsync(request.FiduciaryProductId, ct);
            if (entity is null)
                return Result.Failure<FiduciaryProduct>(FiduciaryProductErrors.FiduciaryProductWithIdNotFound);

            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<FiduciaryProduct>(SharedErrors.UnknownError);
        }
    }
}
