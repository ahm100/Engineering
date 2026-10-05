
namespace Engineering.Application.Services.ContractorContracts.Contracts.GeOperationtFilteredSuggestedPriceHistories;

public record GetOperationFilteredSuggestedPriceHistoriesRequest(
    long? ProjectOperationId,
    long? OperationInfoId,
    DateTime? StartDate,
    DateTime? EndDate,
    long? ContractorId,
    long? CostCenterId,
    long? ProjectId,
    string? FilterData,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
    ) : IHttpRequest;
