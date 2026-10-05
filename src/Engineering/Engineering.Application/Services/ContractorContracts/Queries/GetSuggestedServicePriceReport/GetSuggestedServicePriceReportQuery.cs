using Engineering.Application.Services.ContractorContracts.Contracts.GetSuggestedServicePrice.Enum;
using Engineering.Domain.Entities.ContractorContracts.Enums;

namespace Engineering.Application.Services.ContractorContracts.Queries.GetSuggestedServicePriceReport;

public record GetSuggestedServicePriceExcelExporterQuery(
    List<long> Ids,
    string? StartDate,
    string? EndDate,
    ContractorContractStatus? Status,
    string? ContractorContractType,
    long? ContractorId,
    string? Contractor,
    long? CreatorId,
    string? Creator,
    decimal? Price,
    long? CurrencyId,
    string? Currency,
    int? WorkDonePercent,
    int? WorkDeliveryPercent,
    int? WorkCompletionPercent,
    string? Description,
    long? CompanyId,
    string? CompanyNameFa,
    string[]? OrderBy,
    int PageIndex,
    int PageSize,
    CT ct
) : IQuery<DataResult<List<SuggestedServicePriceEnum>>>;