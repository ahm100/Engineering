namespace Engineering.Application.Services.Contractors.Models.ContractorServices.CreateContractorsService;

public record CreateContractorServicesRequest(long ContractorId,
                                              List<CreateContractorServicesRequestModel> ServiceInfoIds) : IHttpRequest;
