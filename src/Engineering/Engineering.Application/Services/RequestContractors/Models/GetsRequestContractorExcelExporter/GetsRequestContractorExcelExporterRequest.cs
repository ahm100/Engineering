using Engineering.Application.Services.RequestContractors.Models.GetsRequestContractorExcelEnums;
using Engineering.Domain.Entities.RequestContractors.Enums;

namespace Engineering.Application.Services.RequestContractors.Models.GetsRequestContractorExcelExporter;

public record GetsRequestContractorExcelExporterRequest(
        List<long>? Ids,
        List<long>? CostCenterIds,
        List<long>? ProjectIds,
        List<long>? ProjectOperationIds,
        List<long>? ProjectOperationDetailIds,
        List<long>? ServiceInfoIds,
        List<long>? ContractorIds,
        RequestContractorStatus? Status,
        DateTime? FromDate,
        DateTime? ToDate,
        long? CreatorId,
        string? FilterData,
        string[]? OrderBy,
        long? CompanyId,
        List<GetsRequestContractorExcelEnum>? ExcelFilters,
        int PageIndex,
        int PageSize
    ) : IHttpRequest;
