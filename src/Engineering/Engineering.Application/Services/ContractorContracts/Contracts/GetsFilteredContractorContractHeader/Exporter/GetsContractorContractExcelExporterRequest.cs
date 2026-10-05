using Engineering.Application.Services.ContractorContracts.Contracts.GetsFilteredContractorContractHeader.Enum;
using Engineering.Domain.Entities.ContractorContracts.Enums;

namespace Engineering.Application.Services.ContractorContracts.Contracts.GetsFilteredContractorContractHeader.Exporter;

public record GetsContractorContractExcelExporterRequest(
    List<long>? Ids,
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
    List<ContractorContractExcelEnum>? ExcelFilters,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
