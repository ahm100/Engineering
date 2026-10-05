using Engineering.Application.Services.ConsumableVolumes.Models.Products.DataModels;

namespace Engineering.Application.Services.ConsumableVolumes.Queries.Products.GetProductsByProjectOperationId;

public record GetProductsByProjectOperationIdQuery(
    long ProjectOperationId,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<ProductsDataModel>>>;
