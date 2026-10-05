
namespace Engineering.Application.WebServices.MetaDataServices.Cities.Models;

public record Province(
    long Id,
    string Name,
    string Code,
    bool IsActive,
    string EnglishName,
    string Iso,
    bool IsPrimary
    );