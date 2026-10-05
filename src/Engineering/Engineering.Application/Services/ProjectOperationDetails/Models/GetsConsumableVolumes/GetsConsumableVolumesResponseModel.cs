using Engineering.Application.Services.ProjectOperationDetails.Models.DataModels.Responses;

namespace Engineering.Application.Services.ProjectOperationDetails.Models.GetsConsumableVolumes;

public record GetsConsumableVolumesResponseModel(
    List<ExpertDataModel>? ExpertData,
    List<MachineryDataModel>? MachineryData,
    List<ProductDataModel>? ProductData
    );
