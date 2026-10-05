using Engineering.Application.Services.ConsumableVolumes.Models.Products.DataModels;

namespace Engineering.Application.Services.ConsumableVolumes.Models.Products.GetsProductsByFiltered;

public record GetsProductsByFilteredResponse(
    List<ConsumableVolumeProductModel> Data,
    int RowCount
    );
