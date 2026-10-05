using Engineering.Application.Services.Projects.Models.ProjectWarehouseModels;
using Engineering.Domain.Entities.Projects.Enums;

namespace Engineering.Application.Services.Projects.Models.CreateProject;

public record CreateProjectRequest(
    long? ProjectTypeId,
    string ProjectName,
    string? ProjectEnName,
    string? ProjectCode,
    string? Prefix,
    long? EmployerId,
    List<long>? CategoryIds,
    long? CostCenterId,
    List<long>? CostCenterIds,
    long? ProjectManagerId,
    long? PlanningAssistantId,
    long? SupervisorEngineerId,
    long? AdvisorId,
    List<long>? ImplementationAssistants,
    List<long>? TechnicalAssistants,
    ProjectStatus Status,
    bool? Contractual,
    bool CollectiveService,
    decimal? ApprovedBudget,
    long? CityId,
    string? Description,
    string? DescriptionEn,
    string? AddressDescription,
    bool IsActive,
    bool HasProduct,
    long? OrganizationId,
    bool IsOrganizationUnit,
    bool? HaveCostCenter = true,
    List<ProjectWarehouseRequest>? ProjectWarehouses = null
     ) : IHttpRequest;
