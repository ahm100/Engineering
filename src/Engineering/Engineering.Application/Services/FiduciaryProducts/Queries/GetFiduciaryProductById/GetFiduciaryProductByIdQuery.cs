using Engineering.Domain.Entities.FiduciaryProducts;

namespace Engineering.Application.Services.FiduciaryProducts.Queries.GetFiduciaryProductById;

public record GetFiduciaryProductByIdQuery(long FiduciaryProductId) : IQuery<FiduciaryProduct>;
