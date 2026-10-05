
namespace Engineering.Application.Services.Projects.Models.GetProjectByCode;

public record GetProjectByCodeResponse(
    long Id,
    string ProjectName,
    string? ProjectCode,
    string? Prefix,
    bool Contractual,
    bool IsActive
    );
