using Engineering.Domain.Entities.FiduciaryProducts;

namespace Engineering.Application.Services.FiduciaryProducts.Queries.GetFiduciaryProductDetailById;

public record GetFiduciaryProductDetailByIdQuery(long FiduciaryProductDetailId) : IQuery<FiduciaryProductDetail>;
