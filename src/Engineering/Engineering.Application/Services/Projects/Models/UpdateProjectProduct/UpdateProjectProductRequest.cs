using Engineering.Domain.Entities.Projects.Enums;

namespace Engineering.Application.Services.Projects.Models.UpdateProjectProduct;

public record UpdateProjectProductRequest(
    long ProjectId,
    List<UpdateProjectProductModel> Products
     ) : IHttpRequest;

public record UpdateProjectProductModel(
    long? ProjectProductId,
    long? ProductGroupId,
    long? ProductCategoryId,
    decimal RequestQuantity,
    decimal? TolerancePercentage,
    bool? IsActive,
    bool? DefaultManagerSet,
    ProjectProductType ProductType,
    bool IsDelete
     );
