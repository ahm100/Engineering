using Engineering.Domain.Entities.ContractorContracts.Enums;

namespace Engineering.Application.Services.ContractorContracts.Contracts.GetFilteredContractorContracts;

public record GetFilteredContractorContractsRequest(
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
    int PageIndex,
    int PageSize
    ) : IHttpRequest;