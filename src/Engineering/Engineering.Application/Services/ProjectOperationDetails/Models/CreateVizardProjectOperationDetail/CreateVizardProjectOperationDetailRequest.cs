
namespace Engineering.Application.Services.ProjectOperationDetails.Models.CreateVizardProjectOperationDetail;

public record CreateVizardProjectOperationDetailRequest(
    List<long> OperationLocationIds,
    List<CreateVizardProjectOperationDetailRequestModel> RequestModel
     ) : IHttpRequest;
