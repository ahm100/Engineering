
namespace Engineering.Application.Services.Projects.Models.GetProjectByName;

public record GetProjectByNameResponse(
    long Id,
    string ProjectName,
    string? ProjectCode,
    string? Prefix,
    bool Contractual,
    bool IsActive
    );
