
namespace Engineering.Application.WebServices.MetaDataServices.Currencies.Models;

public record Currency(
    long Id,
    string Name,
    bool IsDefault,
    string Iso,
    string Symbol,
    long CountryId
    );
