
using Engineering.Application.Services.ProjectOperationDetails.Models.DataModels.Requests;

namespace Engineering.Application.Services.ProjectOperationDetailContractorServices.Models.UpdateContractorService;

public record UpdateContractorServiceRequest(
    long ProjectOperationDetailId,
    List<UpdateContractorServiceRequestModel> ContractorServiceRequests
     ) : IHttpRequest;
