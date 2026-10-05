using Engineering.Domain.Entities.FiduciaryProducts;

namespace Engineering.Application.Abstractions.Data.FiduciaryProducts;

public interface IFiduciaryProductDetailReturnRepository : IBaseRepository<FiduciaryProductDetailReturn>
{
    Task<FiduciaryProductDetailReturn?> GetByIdAsync(long id, CT ct);
}
