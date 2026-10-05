using Engineering.Domain.Entities.ContractorContracts;

namespace Engineering.Application.Services.ContractorContracts.Queries.GetFixContractorContractByIdIncludeless;

public record GetFixContractorContractByIdIncludelessQuery(
    long Id,
    long CompanyId
    ) : IQuery<ContractorContract?>;
