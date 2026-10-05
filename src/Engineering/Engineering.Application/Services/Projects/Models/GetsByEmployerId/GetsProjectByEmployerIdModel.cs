using Engineering.Application.Services.Projects.Models.ProjectModels;

namespace Engineering.Application.Services.Projects.Models.GetsByEmployerId;

public record GetsProjectByEmployerIdModel(
    long Id,
    string? ProjectCode,
    string ProjectName,
    long? CostCenterId,
    string? CostCenterName,
    ProjectUserModel? Supervisor,
    bool Contractual,
    bool IsActive,
    long? CompanyId,
    string? CompanyNameFa
    );
