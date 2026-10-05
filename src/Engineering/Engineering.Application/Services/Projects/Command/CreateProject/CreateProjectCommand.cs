using Engineering.Application.Services.Projects.Models.ProjectWarehouseModels;
using Engineering.Domain.Entities.Categories;
using Engineering.Domain.Entities.CostCenters;
using Engineering.Domain.Entities.Projects;
using Engineering.Domain.Entities.Projects.Enums;
using Project = Engineering.Domain.Entities.Projects.Project;

namespace Engineering.Application.Services.Projects.Commands.CreateProject;

public record CreateProjectCommand(
    ProjectType? ProjectType,
    string ProjectName,
    string? ProjectEnName,
    long? EmployerId,
    List<Category>? Categories,
    CostCenter? CostCenter,
    List<CostCenter>? CostCentersList,
    long? SupervisorEngineer,
    long? Advisor,
    long? ProjectManager,
    long? PlanningAssistant,
    List<long>? ImplementationAssistants,
    List<long>? TechnicalAssistants,
    ProjectStatus Status,
    bool Contractual,
    bool CollectiveService,
    bool IsActive,
    string ProjectCode,
    string? Prefix,
    decimal? ApprovedBudget,
    long? CityId,
    string? Description,
    string? DescriptionEn,
    string? AddressDescription,
    long? CompanyId,
    bool HasProducts,
    long? OrganizationId,
    bool IsOrganizationUnit,
    List<ProjectWarehouseRequest>? ProjectWarehouses
    ) : ICommand<Project>;
