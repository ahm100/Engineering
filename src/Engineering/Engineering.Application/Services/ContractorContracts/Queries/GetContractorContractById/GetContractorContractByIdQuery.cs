using Engineering.Domain.Entities.ContractorContracts;

namespace Engineering.Application.Services.ContractorContracts.Queries.GetContractorContractById;

public record GetContractorContractByIdQuery(
    long ContractorContractId,
    long CompanyId
    ) : IQuery<ContractorContract?>;
