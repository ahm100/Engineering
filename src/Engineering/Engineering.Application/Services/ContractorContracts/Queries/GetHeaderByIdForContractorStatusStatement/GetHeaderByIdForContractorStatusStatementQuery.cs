using Engineering.Domain.Entities.ContractorContracts;

namespace Engineering.Application.Services.ContractorContracts.Queries.GetHeaderByIdForContractorStatusStatement;

public record GetHeaderByIdForContractorStatusStatementQuery(
    long Id
    ) : IQuery<ContractorContractHeader?>;
