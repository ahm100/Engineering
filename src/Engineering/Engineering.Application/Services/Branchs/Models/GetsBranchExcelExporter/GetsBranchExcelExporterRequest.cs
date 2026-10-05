using Engineering.Application.Services.Branchs.Models.GetsBranchExcelEnum;

namespace Engineering.Application.Services.Branchs.Models.GetsBranchExcelExporter;

public record GetsBranchExcelExporterRequest(
    List<long>? Ids,
    string? FilterData,
    long? CategoryId,
    string? BranchName,
    string? BranchCode,
    bool? IsActive,
    string[]? OrderBy,
    List<BranchExcelEnum>? ExcelFilters,
    int PageIndex,
    int PageSize)
    : IHttpRequest;