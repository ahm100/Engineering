
namespace Engineering.Application.WebServices.MetaDataServices.Measureunits.Models;

public record Measureunit(
    long Id,
    string Name,
    long MeasureUnitGroupId,
    decimal ConversionFactor,
    decimal Tolerance,
    bool IsActive,
    bool IsPrimary
    );
