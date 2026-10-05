namespace Engineering.Application.Services.Seasons.Models.SeasonModels;

public record GetsActiveSeasonModel
{
    public long Id { get; set; }
    public string SeasonName { get; set; } = string.Empty;
    public string SeasonCode { get; set; } = string.Empty;
}
