using Engineering.Application.Services.DailyProjectOperations.Models.GetsDetailedDailyProjectOperationExcelEnums;
using Engineering.Domain.Entities.ProjectOperationDetails.Enums;

namespace Engineering.Application.Services.DailyProjectOperations.Models.GetsDetailedDailyProjectOperationExcelExporter;

public record GetsDetailedDailyProjectOperationExcelExporterRequest(
     List<long>? Ids,
     List<long>? CostCenterIds,
     List<long>? ProjectIds,
     List<long>? ProjectOperationIds,
     List<long>? ProjectOperationDetailIds,
     List<long>? ContractorIds,
     List<long>? ServiceInfoIds,
     List<long>? CreatorIds,
     DateTime? StartDate,
     DateTime? EndDate,
     DateTime? FromDate,
     DateTime? ToDate,
     List<ProjectOperationDetailStatus>? ProjectOperationDetailStatus,
     List<ProjectOperationDetailStatus>? Status,
     string? FilterData,
     List<DetailedDailyProjectOperationsExcelEnum>? ExcelFilters,
     string[]? OrderBy,
     int PageIndex,
     int PageSize) : IHttpRequest;
