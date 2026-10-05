using Engineering.Application.Services.Projects.Models.ProjectWarehouseModels;
using Engineering.Domain.Entities.Projects.Enums;

namespace Engineering.Application.Services.Projects.Models.UpdateProject;

public record UpdateProjectRequest(
    long Id,
    long? ProjectTypeId,
    string? ProjectName,
    string? ProjectEnName,
    string? ProjectCode,
    string? Prefix,
    long? EmployerId,
    long? CostCenterId,
    List<long>? CostCenterIds,
    List<long>? DeleteCostCenterIds,
    List<long>? CategoryIds,
    List<long?>? DeleteCategoryIds,
    long ProjectManagerId,
    long? PlanningAssistantId,
    long? SupervisorEngineerId,
    long? ThirdPartyId,
    long? AdvisorId,
    ProjectStatus Status,
    bool? Contractual,
    bool CollectiveService,
    bool IsActive,
    List<long>? ImplementationAssistants,
    List<long>? TechnicalAssistants,
    decimal? ApprovedBudget,
    long? CityId,
    string? Description,
    string? DescriptionEn,
    string? AddressDescription,
    bool? HasProduct,
    long? OrganizationId,
    bool IsOrganizationUnit,
    List<ProjectWarehouseRequest>? ProjectWarehouses = null
     ) : IHttpRequest;
