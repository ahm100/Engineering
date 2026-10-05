
namespace Engineering.Application.Services.Projects.Models.ProjectModels;

public record ProjectUserModel(
    long? Id,
    long? UserId,
    string? FullName,
    string? AvatarUrl,
    string? OrganizationCode
    );


