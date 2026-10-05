namespace Engineering.Application.Services.Projects.Models.ProjectModels;

public record GetUserInfo(
    long Id,
    bool? IsIndividual,
    long? UserId,
    string? FullName,
    string? Nickname,
    string? AvatarUrl,
    string? OrganizationCode
    );


