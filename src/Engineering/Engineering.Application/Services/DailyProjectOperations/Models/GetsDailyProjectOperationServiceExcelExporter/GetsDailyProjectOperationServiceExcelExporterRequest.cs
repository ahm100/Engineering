using Engineering.Application.Services.DailyProjectOperations.Models.GetsDailyProjectOperationServiceExcelEnums;
using Engineering.Domain.Entities.ProjectOperationDetails.Enums;

namespace Engineering.Application.Services.DailyProjectOperations.Models.GetsDailyProjectOperationServiceExcelExporter;

public record GetsDailyProjectOperationServiceExcelExporterRequest(
                                                    List<long>? Ids,
                                                    List<long>? CostCenterIds,
                                                    List<long>? ProjectIds,
                                                    List<long>? ProjectOperationIds,
                                                    List<long>? ProjectOperationDetailIds,
                                                    List<long>? ContractorIds,
                                                    List<long>? ServiceInfoIds,
                                                    List<long>? MeasurUnitIds,
                                                    DateTime? StartDate,
                                                    DateTime? EndDate,
                                                    DateTime? FromDate,
                                                    DateTime? ToDate,
                                                    ProjectOperationDetailStatus? Status,
                                                    string? FilterData,
                                                    string? FilterServiceInfo,
                                                    List<DailyProjectOperationServicesExcelEnum>? ExcelFilters,
                                                    string[]? OrderBy,
                                                    int PageIndex,
                                                    int PageSize) : IHttpRequest;
