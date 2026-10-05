using Engineering.Application.Services.ProjectOperationDetails.Models.DataModels.Requests;

namespace Engineering.Application.Services.ConsumableVolumes.Models.UpdateConsumableVolumes;

public record UpdateConsumableVolumesRequest(
    long ProjectOperationDetailId,
    List<UpdateExpertRequestModel>? ExpertRequests,
    List<UpdateMachineryRequestModel>? MachineryRequests,
    List<UpdateProductRequestModel>? ProductRequests
     ) : IHttpRequest;
