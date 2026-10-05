using Engineering.Application.Services.MachineTypes.Models.GetsMachineTypeExcelEnum;

namespace Engineering.Application.Services.MachineTypes.Models.GetsMachineTypeExcelExporter;

public record GetsMachineTypeExcelExporterRequest(
    List<long>? Ids,
    string? FilterData,
    bool? IsActive,
    List<MachineTypeExcelEnum>? ExcelFilters,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
