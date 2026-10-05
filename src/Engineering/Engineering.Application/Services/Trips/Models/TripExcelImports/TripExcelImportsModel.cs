namespace Engineering.Application.Services.Trips.Models.TripExcelImports;

public record TripExcelImportsModel
{
    public string TripName { get; private set; } = string.Empty;
    public string TripCode { get; private set; } = string.Empty;
    public bool IsActive { get; private set; } = true;
}
