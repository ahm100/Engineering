namespace Engineering.Application.Services.Trips.Models.GetByCode;

public record GetTripByCodeResponse
{
    public long Id { get; set; }
    public string TripName { get; set; } = string.Empty;
    public string TripCode { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public long? CompanyId { get; set; }
    public string? CompanyNameFa { get; set; } = string.Empty;
}