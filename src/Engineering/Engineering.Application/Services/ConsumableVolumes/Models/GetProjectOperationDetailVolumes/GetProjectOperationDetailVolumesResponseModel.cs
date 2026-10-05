using Engineering.Application.Services.ProjectOperationDetails.Models.DataModels.Responses;

namespace Engineering.Application.Services.ConsumableVolumes.Models.GetProjectOperationDetailVolumes;

public record GetProjectOperationDetailVolumesResponseModel(
    List<ExpertDataModel>? ExpertData,
    List<MachineryDataModel>? MachineryData,
    List<ProductDataModel>? ProductData
    );
