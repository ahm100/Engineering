using Engineering.Application.Services.MachineriesGroups.Models.GetsMachineriesGroupExcelEnum;

namespace Engineering.Application.Services.MachineriesGroups.Models.GetsMachineriesGroupExcelExporter;

public record GetsMachineriesGroupExcelExporterRequest(
    List<long>? Ids,
    string? FilterData,
    string? Code,
    string? Name,
    bool? IsActive,
    List<MachineriesGroupExcelEnum>? ExcelFilters,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
