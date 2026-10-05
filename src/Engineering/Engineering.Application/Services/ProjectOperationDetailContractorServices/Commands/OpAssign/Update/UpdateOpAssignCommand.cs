using Engineering.Domain.Entities.ProjectOperationDetails;

namespace Engineering.Application.Services.ProjectOperationDetailContractorServices.Commands.OpAssign.Update;

public record UpdateOpAssignCommand(
    ProjectOperationDetailContractorService Entity,
    long ContractorId,
    decimal Volume,
    decimal ContractAllocatedConstructionQuantity
) : ICommand<ProjectOperationDetailContractorService>;
