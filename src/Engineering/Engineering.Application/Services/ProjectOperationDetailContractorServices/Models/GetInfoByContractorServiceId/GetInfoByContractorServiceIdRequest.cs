namespace Engineering.Application.Services.ProjectOperationDetailContractorServices.Models.GetInfoByContractorServiceId;


public record GetInfoByContractorServiceIdRequest(
    List<long?> GetInfoByContractorServiceId
     ) : IHttpRequest;
