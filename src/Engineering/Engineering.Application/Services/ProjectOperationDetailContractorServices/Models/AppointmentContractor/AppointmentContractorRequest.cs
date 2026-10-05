
namespace Engineering.Application.Services.ProjectOperationDetailContractorServices.Models.AppointmentContractor;

public record AppointmentContractorRequest(
    long ContractorId,
    List<long> ContractorServiceIds
     ) : IHttpRequest;
