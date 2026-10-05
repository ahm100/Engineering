using Engineering.Domain.Entities.ContractorContracts.Enums;

namespace Engineering.Application.Services.ContractorContracts.Contracts.GetsFilteredContractorContractHeader;

public record GetsFilteredContractorContractHeaderRequest(
    long? ContractorId,
    long? CostCenterId,
    List<long>? ProjectIds,
    long? ProjectManagerId,
    List<long>? ProjectOperationIds,
    DateTime? FromDate,
    DateTime? ToDate,
    List<ContractorContractStatus>? Statuses,
    List<ContractorContractStatus>? RemoveStatuses,
    long? ContractorContractTypeId,
    string? FilterData,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
    ) : IHttpRequest;
