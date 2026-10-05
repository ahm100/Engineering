using Engineering.Domain.Entities.ContractorContracts;

namespace Engineering.Application.Services.ContractorContracts.Queries.GetContractorContractHeaderById;

public record GetContractorContractHeaderByIdQuery(
    long Id
    ) : IQuery<ContractorContractHeader?>;
