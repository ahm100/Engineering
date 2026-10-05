using Engineering.Application.Services.Machineries.Models.GetsMachineryExcelEnum;

namespace Engineering.Application.Services.Machineries.Models.GetsMachineryExcelExporter;

public record GetsMachineryExcelExporterRequest(
    List<long>? Ids,
    string? FilterData,
    long? MachineriesGroupId,
    string? MachineryName,
    string? MachineryCode,
    bool? IsActive,
    List<MachineryExcelEnum>? ExcelFilters,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
