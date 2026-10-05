using Engineering.Domain.Entities.ContractorContracts;
using Engineering.Domain.Entities.ContractorContracts.Enums;

namespace Engineering.Application.Services.ContractorContracts.Queries.GetFilteredContractorContracts;

public record GetFilteredContractorContractsQuery(
    long? ContractorId,
    long? CostCenterId,
    List<long>? ProjectIds,
    List<long>? ProjectOperationIds,
    List<long>? EmployerContracts,
    DateTime? FromDate,
    DateTime? ToDate,
    ContractorContractStatus? Status,
    long? ContractorContractTypeId,
    string? FilterData,
    long CompanyId,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<ContractorContract>>>;
