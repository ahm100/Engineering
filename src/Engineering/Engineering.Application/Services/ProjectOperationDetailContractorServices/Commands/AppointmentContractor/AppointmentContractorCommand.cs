using ProjectOperationDetailContractorService = Engineering.Domain.Entities.ProjectOperationDetails.ProjectOperationDetailContractorService;

namespace Engineering.Application.Services.ProjectOperationDetailContractorServices.Commands.AppointmentContractor;

public record AppointmentContractorCommand(
    long Id,
    long ContractorId
    ) : ICommand<ProjectOperationDetailContractorService>;