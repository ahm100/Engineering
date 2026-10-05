using Engineering.Domain.Entities.ContractorContracts;

namespace Engineering.Application.Services.ContractorContracts.Queries.GetServiceContractorContractByIdIncludeless;

public record GetServiceContractorContractByIdIncludelessQuery(
    long Id,
    long CompanyId
    ) : IQuery<ContractorContract?>;
