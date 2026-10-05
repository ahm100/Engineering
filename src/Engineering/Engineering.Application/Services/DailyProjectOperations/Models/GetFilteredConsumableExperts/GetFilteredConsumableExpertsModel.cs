namespace Engineering.Application.Services.DailyProjectOperations.Models.GetFilteredConsumableExperts;

public record GetFilteredConsumableExpertsModel
{
    public long ConsumableVolumeExpertId { get; set; }
    public long? SkillId { get; set; }
    public string? Name { get; set; } = string.Empty;
    public string? Code { get; set; } = string.Empty;
    public decimal Number { get; set; }
    public List<GetFilteredConsumableExpertsDetailModel>? Details { get; set; } = new();
}
