namespace Engineering.Application.Services.EngineeringConfigs.Contracts.GetConfigHistoryByConfigId;

public record GetConfigHistoryByConfigIdResponse(
    List<GetConfigHistoryByConfigIdModel> Data,
    int RowCount);

public class GetConfigHistoryByConfigIdModel
{
    public long Id { get; set; }
    public long EngineeringConfigId { get; set; }
    public bool SendTelegramMessage { get; set; }
    public bool ProjectThirdParties { get; set; }
    public bool IsActive { get; set; }
    public long CreatorId { get; set; }
    public string Creator { get; set; } = string.Empty;
    public DateTime Created { get; set; }
    public string CreatedShamsi => Created.ToShamsi();
    public DateTime? Updated { get; set; }
    public string? UpdatedShamsi => Updated.ToShamsi();
}