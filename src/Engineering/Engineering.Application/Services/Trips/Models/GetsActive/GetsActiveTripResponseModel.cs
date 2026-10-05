namespace Engineering.Application.Services.Trips.Models.GetsActive;

public record GetsActiveTripResponseModel
{
    public long Id { get; set; }
    public string TripName { get; set; } = string.Empty;
    public string TripCode { get; set; } = string.Empty;
    public long? CompanyId { get; set; }
    public string? CompanyNameFa { get; set; } = string.Empty;
}
