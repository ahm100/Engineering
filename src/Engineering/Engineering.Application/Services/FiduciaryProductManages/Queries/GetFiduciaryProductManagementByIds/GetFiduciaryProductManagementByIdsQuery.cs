using Engineering.Domain.Entities.FiduciaryProducts;

namespace Engineering.Application.Services.FiduciaryProductManages.Queries.GetFiduciaryProductManagementById;

public record GetFiduciaryProductManagementByIdsQuery(List<long> Ids) : IQuery<List<FiduciaryProductDetailManagement>>;
