using Engineering.Application.Services.ContractorMachineries.Models.GetsContractorMachineryExcelEnum;
using Engineering.Domain.Entities.ContractorMachineries.Enums;

namespace Engineering.Application.Services.ContractorMachineries.Models.GetsContractorMachineryExcelExporter;

public record GetsContractorMachineryExcelExporterRequest(
    List<long>? Ids,
    List<long>? MachineryIds,
    List<long>? ContractorIds,
    ContractorMachineryUnit? Unit,
    DateTime? FromDate,
    DateTime? ToDate,
    string? FilterData,
    bool? IsActive,
    long? CompanyId,
    string[]? OrderBy,
    List<ContractorMachineryExcelEnum>? ExcelFilters,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
