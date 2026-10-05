
namespace Engineering.Application.Services.TransportationRequests.Models.GetSnapById;

public record SnapNumberPlatesModel
{
    public string? Part1 { get; set; } = string.Empty;
    public string? Part2 { get; set; } = string.Empty;
    public string? Part3 { get; set; } = string.Empty;
    public string? Letter { get; set; } = string.Empty;
}