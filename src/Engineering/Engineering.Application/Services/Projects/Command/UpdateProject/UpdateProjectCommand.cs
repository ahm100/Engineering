using Engineering.Domain.Entities.Projects.Enums;
using Project = Engineering.Domain.Entities.Projects.Project;

namespace Engineering.Application.Services.Projects.Commands.UpdateProject;

public record UpdateProjectCommand(
    Project Project,
    string? NewCode,
    string? Prefix,
    string ProjectName,
    string? ProjectEnName,
    long? EmployerId,
    long? Advisor,
    long? SupervisorEngineer,
    long? ProjectManager,
    long? PlanningAssistant,
    ProjectStatus Status,
    bool Contractual,
    bool CollectiveService,
    bool IsActive,
    List<long>? ImplementationAssistants,
    List<long>? TechnicalAssistants,
    decimal? ApprovedBudget,
    long? CityId,
    string? Description,
    string? DescriptionEn,
    string? AddressDescription,
    long? CompanyId,
    bool? HasProduct,
    long? OrganizationId,
    bool IsOrganizationUnit
    ) : ICommand<Project>;
