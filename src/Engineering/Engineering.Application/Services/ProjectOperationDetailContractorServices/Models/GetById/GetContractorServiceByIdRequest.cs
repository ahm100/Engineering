
namespace Engineering.Application.Services.ProjectOperationDetailContractorServices.Models.GetContractorServiceById;

public record GetContractorServiceByIdRequest(
    long Id
     ) : IHttpRequest;