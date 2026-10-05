namespace Engineering.Application.Services.DailyProjectOperations.Models.GetDailyProjectOperationById;

public record GetDailyProjectOperationByIdExpertModel
{
    public long Id { get; set; }
    public long ThirdPartyId { get; set; }
    public string? ThirdParty { get; set; } = string.Empty;
    public string FinalValue { get; set; } = string.Empty;
    public string UnusedValue { get; set; } = string.Empty;
    public long ConsumableVolumeExpertId { get; set; }
    public long? SkillId { get; set; }
    public string? Skill { get; set; } = string.Empty;
}
