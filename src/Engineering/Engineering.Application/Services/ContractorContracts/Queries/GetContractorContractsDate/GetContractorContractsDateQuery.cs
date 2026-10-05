using Engineering.Application.Services.ContractorContracts.Contracts.GetContractorContractsDate;

namespace Engineering.Application.Services.ContractorContracts.Queries.GetContractorContractsDate;

public record GetContractorContractsDateQuery(
    long ProjectId,
    long ContractorId,
    long CompanyId
    ) : IQuery<GetContractorContractsDateResponse?>;
