
namespace Engineering.Application.WebServices.MetaDataServices.Cities.Models;

public record City(
    long Id,
    string Name,
    string Code,
    bool IsActive,
    string EnglishName,
    string? Iso,
    Province Province
    );
