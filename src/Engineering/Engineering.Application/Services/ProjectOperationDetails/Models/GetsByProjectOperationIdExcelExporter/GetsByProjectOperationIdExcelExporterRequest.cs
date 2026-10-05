using Engineering.Application.Services.ProjectOperationDetails.Models.GetsByProjectOperationIdExcelEnums;
using Engineering.Domain.Entities.ProjectOperationDetails.Enums;

namespace Engineering.Application.Services.ProjectOperationDetails.Models.GetsByProjectOperationIdExcelExporter;

public record GetsByProjectOperationIdExcelExporterRequest(
                                                    List<long>? Ids,
                                                    long ProjectOperationId,
                                                    string? PrivateName,
                                                    string? PrivateCode,
                                                    string? FilterData,
                                                    long? EmployerId,
                                                    ProjectOperationDetailStatus? Status,
                                                    List<long>? ContractorIds,
                                                    DateTime? CreateDate,
                                                    DateTime? StartDate,
                                                    DateTime? EndDate,
                                                    List<long>? ServiceInfoIds,
                                                    List<long>? ImplementationAssistantIds,
                                                    List<long>? TechnicalAssistantIds,
                                                    long? CreatorId,
                                                    List<ProjectOperationDetailExcelEnum>? ExcelFilters,
                                                    string[]? OrderBy,
                                                    int PageIndex,
                                                    int PageSize) : IHttpRequest;
