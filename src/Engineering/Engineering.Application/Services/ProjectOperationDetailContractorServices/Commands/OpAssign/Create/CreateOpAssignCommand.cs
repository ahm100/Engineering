using Engineering.Domain.Entities.ProjectOperationDetails;

namespace Engineering.Application.Services.ProjectOperationDetailContractorServices.Commands.OpAssign.Create;

public record CreateOpAssignCommand(
    ProjectOperationDetail ProjectOperationDetail,
    long ContractorId,
    decimal? Volume,
    decimal ContractAllocatedConstructionQuantity
) : ICommand<ProjectOperationDetailContractorService>;
