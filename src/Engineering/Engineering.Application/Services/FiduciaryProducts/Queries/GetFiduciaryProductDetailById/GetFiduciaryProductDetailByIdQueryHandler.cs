using Engineering.Application.Abstractions.Data.FiduciaryProducts;
using Engineering.Domain.Entities.FiduciaryProducts;

namespace Engineering.Application.Services.FiduciaryProducts.Queries.GetFiduciaryProductDetailById;

public class GetFiduciaryProductDetailByIdQueryHandler : IQueryHandler<GetFiduciaryProductDetailByIdQuery, FiduciaryProductDetail>
{
    private readonly ILogger<GetFiduciaryProductDetailByIdQueryHandler> _logger;
    private readonly IFiduciaryProductDetailRepository _repository;

    public GetFiduciaryProductDetailByIdQueryHandler(ILogger<GetFiduciaryProductDetailByIdQueryHandler> logger, IFiduciaryProductDetailRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<FiduciaryProductDetail?>> Handle(GetFiduciaryProductDetailByIdQuery request, CT ct)
    {
        try
        {
            var entity = await _repository.GetByIdAsync(request.FiduciaryProductDetailId, ct);
            if (entity is null)
                return Result.Failure<FiduciaryProductDetail>(FiduciaryProductDetailErrors.FiduciaryProductDetailWithIdNotFound);

            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<FiduciaryProductDetail>(SharedErrors.UnknownError);
        }
    }
}
