
namespace Engineering.Application.Services.ContractorContracts.Contracts.GetServiceFilteredSuggestedPriceHistories;

public record GetServiceFilteredSuggestedPriceHistoriesRequest(
    long? ProjectOperationServiceId,
    long? ServiceInfoId,
    DateTime? StartDate,
    DateTime? EndDate,
    long? ContractorId,
    long? CostCenterId,
    long? ProjectId,
    string? FilterData,
    string[]? OrderBy,
    int PageIndex,
    int PageSize) : IHttpRequest;
