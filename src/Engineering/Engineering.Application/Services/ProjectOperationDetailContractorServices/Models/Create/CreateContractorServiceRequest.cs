
using Engineering.Domain.Entities.ProjectOperationDetails.Enums;

namespace Engineering.Application.Services.ProjectOperationDetailContractorServices.Models.CreateContractorService;

public record CreateContractorServiceRequest(
    long ProjectOperationDetailId,
    long ServiceInfoId,
    long? ProjectServiceDetailId,
    long? ContractorId,
    decimal Volume,
    string TimeSpant,
    bool IsActive = true,
    PODContractorServiceType? Type = null
     ) : IHttpRequest;
