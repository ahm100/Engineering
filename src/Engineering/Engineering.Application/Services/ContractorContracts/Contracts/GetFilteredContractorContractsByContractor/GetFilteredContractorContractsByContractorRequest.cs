namespace Engineering.Application.Services.ContractorContracts.Contracts.GetFilteredContractorContractsByContractor;

public record GetFilteredContractorContractsByContractorRequest(
    long ContractorId,
    long? CostCenterId,
    List<long>? ProjectIds,
    List<long>? ContractIds,
    DateTime? FromDate,
    DateTime? ToDate,
    string? FilterData,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
    ) : IHttpRequest;