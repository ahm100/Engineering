
namespace Engineering.Application.WebServices.MetaDataServices.Banks.Models;

public record Bank(
    long Id,
    string Code,
    string Name,
    string logoUrl,
    bool IsActive
    );
