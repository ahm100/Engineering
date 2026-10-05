using Engineering.Domain.Entities.Projects.Enums;

namespace Engineering.Application.Services.Projects.Models.CreateProjectProduct;

public record CreateProjectProductRequest(
    long ProjectId,
    long? CostCenterId,
    List<CreateProjectProductModel> Products
    ) : IHttpRequest;

public record CreateProjectProductModel(
    long? ProductGroupId,
    long? ProductCategoryId,
    decimal RequestQuantity,
    decimal TolerancePercentage,
    bool? IsActive,
    bool? DefaultManagerSet,
    ProjectProductType ProductType
     ) : IHttpRequest;