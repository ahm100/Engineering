using Engineering.Domain.Entities.ContractorContracts;

namespace Engineering.Application.Services.ContractorContracts.Queries.GetsContractorContractByContractorId;

public record GetsContractorContractByContractorIdQuery(
    long ContractorId,
    long? CostCenterId,
    List<long>? Projects,
    string? FilterData,
    string[]? OrderBy,
    int PageIndex,
    int PageSize,
    long CompanyId
    ) : IQuery<DataResult<List<ContractorContract>>>;
