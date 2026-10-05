using Engineering.Domain.Entities.FiduciaryProducts;

namespace Engineering.Application.Abstractions.Data.FiduciaryProducts;

public interface IFiduciaryProductDetailManagementRepository : IBaseRepository<FiduciaryProductDetailManagement>
{
    Task<List<FiduciaryProductDetailManagement>> GetByIdsAsync(List<long> ids, CT ct);
}
