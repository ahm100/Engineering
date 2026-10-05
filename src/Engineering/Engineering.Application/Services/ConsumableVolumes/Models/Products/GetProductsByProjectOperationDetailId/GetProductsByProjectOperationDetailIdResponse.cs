using Engineering.Application.Services.ConsumableVolumes.Models.Products.DataModels;

namespace Engineering.Application.Services.ConsumableVolumes.Models.Products.GetProductsByProjectOperationDetailId;

public record GetProductsByProjectOperationDetailIdResponse(
    List<ConsumableVolumeProductModel> Data,
    int RowCount
    );
