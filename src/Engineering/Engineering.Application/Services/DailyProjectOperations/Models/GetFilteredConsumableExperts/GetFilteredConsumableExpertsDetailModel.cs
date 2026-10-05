namespace Engineering.Application.Services.DailyProjectOperations.Models.GetFilteredConsumableExperts;

public record GetFilteredConsumableExpertsDetailModel
{
    public long? Id { get; set; }
    public string? FullName { get; set; } = string.Empty;
}
