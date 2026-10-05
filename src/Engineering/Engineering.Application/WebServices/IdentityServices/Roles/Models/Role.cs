
namespace Engineering.Application.IdentityServices.Roles.Models;

public record Role(
    long Id,
    string Name,
    string? Description,
    string? Scope,
    int Status
    );
