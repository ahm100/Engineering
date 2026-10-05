using Engineering.Domain.Entities.ContractorContracts;

namespace Engineering.Application.Services.ContractorContracts.Queries.GetsHeaderForContractorStatusStatement;

public record GetsHeaderForContractorStatusStatementQuery(
    long ContractorId,
    long ProjectId,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<ContractorContractHeader>>>;
