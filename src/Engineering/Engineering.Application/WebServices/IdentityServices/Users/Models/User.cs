
namespace Engineering.Application.IdentityServices.Users.Models;

public record User(
    long Id,
    string Mobile,
    string FirstName,
    string LastName,
    string FullName,
    string OrganizationCode,
    string IdentityNo,
    int Status,
    string StatusText,
    List<long>? RoleIds
    );
