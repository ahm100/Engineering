using Engineering.Application.Services.ConsumableVolumes.Models.Products.DataModels;

namespace Engineering.Application.Services.ConsumableVolumes.Models.Products.GetProductsByProjectOperationId;

public record GetProductsByProjectOperationIdResponse(
    List<ConsumableVolumeProductModel> Data,
    int RowCount
    );
