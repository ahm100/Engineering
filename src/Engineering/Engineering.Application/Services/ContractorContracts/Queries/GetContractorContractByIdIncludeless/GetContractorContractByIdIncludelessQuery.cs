using Engineering.Domain.Entities.ContractorContracts;

namespace Engineering.Application.Services.ContractorContracts.Queries.GetContractorContractByIdIncludeless;

public record GetContractorContractByIdIncludelessQuery(
    long Id,
    long CompanyId
    ) : IQuery<ContractorContract?>;
