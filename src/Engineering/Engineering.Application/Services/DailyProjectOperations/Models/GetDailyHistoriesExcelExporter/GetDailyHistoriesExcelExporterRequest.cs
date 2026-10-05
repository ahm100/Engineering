using Engineering.Application.Services.DailyProjectOperations.Models.GetDailyHistoriesExcelEnums;

namespace Engineering.Application.Services.DailyProjectOperations.Models.GetDailyHistoriesExcelExporter;

public record GetDailyHistoriesExcelExporterRequest(
                                                    List<long>? Ids,
                                                    long ProjectOperationDetailId,
                                                    DateTime? StartDate,
                                                    DateTime? EndDate,
                                                    string? FilterData,
                                                    long? CreatorId,
                                                    List<DailyProjectOperationsExcelEnum>? ExcelFilters,
                                                    string[]? OrderBy,
                                                    int PageIndex,
                                                    int PageSize) : IHttpRequest;
