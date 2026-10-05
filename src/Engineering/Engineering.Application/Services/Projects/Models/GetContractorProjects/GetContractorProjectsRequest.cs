using Engineering.Domain.Entities.Projects.Enums;

namespace Engineering.Application.Services.Projects.Models.GetContractorProjects;

public record GetContractorProjectsRequest(
    long ContractorId,
    List<ProjectStatus>? Statuses,
    long? CostCenterId,
    string? FilterData,
    int PageIndex,
    int PageSize,
    bool? HaveCostCenter = true,
    bool? IsOrganizationUnit = false
     ) : IHttpRequest;
