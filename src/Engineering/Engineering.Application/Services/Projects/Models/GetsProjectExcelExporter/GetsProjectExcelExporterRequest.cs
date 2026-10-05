using Engineering.Application.Services.Projects.Models.GetsProjectExcelEnum;
using Engineering.Domain.Entities.Projects.Enums;

namespace Engineering.Application.Services.Projects.Models.GetsProjectExcelExporter;

public record GetsProjectExcelExporterRequest(
    List<long>? Ids,
    string? FilterData,
    long? EmployerId,
    List<long>? CostCenterId,
    long? ProjectTypeId,
    List<long>? CategoryIds,
    long? ProjectManagerId,
    long? PlanningAssistantId,
    long? ThirdPartyId,
    long? SupervisorEngineerId,
    long? AdvisorId,
    long? ImplementationAssistantId,
    long? TechnicalAssistantId,
    ProjectStatus? Status,
    List<ProjectStatus>? Statuses,
    List<ProjectExcelEnum>? ExcelFilters,
    bool? IsActive,
    string[]? OrderBy,
    int PageIndex,
    int PageSize,
    bool? HaveCostCenter = true,
    bool? IsOrganizationUnit = false
     ) : IHttpRequest;