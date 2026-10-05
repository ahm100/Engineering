using Engineering.Domain.Entities.ContractorContracts;

namespace Engineering.Application.Services.ContractorContracts.Queries.GetContractorContractHeaderByIdIncludeless;

public record GetContractorContractHeaderByIdIncludelessQuery(
    long Id
    ) : IQuery<ContractorContractHeader?>;
