using Engineering.Domain.Entities.ContractorContracts;

namespace Engineering.Application.Services.ContractorContracts.Queries.GetFilteredContractorContractsByContractor;

public record GetFilteredContractorContractsByContractorQuery(
    long ContrctorId,
    long? CostCenterId,
    List<long>? Projects,
    List<long>? Contracts,
    DateTime? FromDate,
    DateTime? ToDate,
    string? FilterData,
    long CompanyId,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<ContractorContract>>>;
