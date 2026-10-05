using ContractorMachinery = Engineering.Domain.Entities.ContractorMachineries.ContractorMachinery;

namespace Engineering.Application.Services.ContractorMachineries.Queries.GetsByContractorId;

public record GetsByContractorIdQuery(
    long ContractorId,
    string? FilterData,
    bool? IsActive,
    long? CompanyId,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<ContractorMachinery>>>;