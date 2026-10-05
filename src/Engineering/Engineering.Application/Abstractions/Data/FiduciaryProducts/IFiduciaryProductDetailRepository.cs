using Engineering.Application.Services.FiduciaryProductManages.Models.GetFiduciaryProductDetailReturnByDetailId;
using Engineering.Domain.Entities.FiduciaryProducts;

namespace Engineering.Application.Abstractions.Data.FiduciaryProducts;

public interface IFiduciaryProductDetailRepository : IBaseRepository<FiduciaryProductDetail>
{
    Task<FiduciaryProductDetail?> GetByIdAsync(long id, CT ct);

    Task<GetFiduciaryProductDetailReturnByDetailIdResponse?> GetDetailReturnById(long id, CT ct);
}
