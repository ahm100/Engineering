using Engineering.Application.Abstractions.Data.FiduciaryProducts;
using Engineering.Domain.Entities.FiduciaryProducts;

namespace Engineering.Application.Services.FiduciaryProductManages.Queries.GetFiduciaryProductManagementById;

public class GetFiduciaryProductManagementByIdsQueryHandler : IQueryHandler<GetFiduciaryProductManagementByIdsQuery, List<FiduciaryProductDetailManagement>>
{
    private readonly ILogger<GetFiduciaryProductManagementByIdsQueryHandler> _logger;
    private readonly IFiduciaryProductDetailManagementRepository _repository;

    public GetFiduciaryProductManagementByIdsQueryHandler(ILogger<GetFiduciaryProductManagementByIdsQueryHandler> logger, IFiduciaryProductDetailManagementRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<List<FiduciaryProductDetailManagement>?>> Handle(GetFiduciaryProductManagementByIdsQuery request, CT ct)
    {
        try
        {
            var entity = await _repository.GetByIdsAsync(request.Ids, ct);
            if (entity is null || entity.Count <= 0)
                return Result.Failure<List<FiduciaryProductDetailManagement>>(FiduciaryProductDetailManagementErrors.FiduciaryProductDetailManagementWithIdNotFound);

            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<List<FiduciaryProductDetailManagement>>(SharedErrors.UnknownError);
        }
    }
}
