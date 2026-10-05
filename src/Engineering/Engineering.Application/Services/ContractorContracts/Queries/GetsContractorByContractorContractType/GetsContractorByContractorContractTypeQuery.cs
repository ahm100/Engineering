using Engineering.Domain.Entities.ContractorContracts;

namespace Engineering.Application.Services.ContractorContracts.Queries.GetsContractorByContractorContractType;

public record GetsContractorByContractorContractTypeQuery(
    long ContractorContractTypeId,
    long CompanyId
    ) : IQuery<DataResult<List<ContractorContract>>>;
