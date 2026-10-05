namespace Engineering.Application.Services.EngineeringConfigs.Contracts.GetActiveConfig;

public class GetActiveConfigResponse
{
    public long CompanyId { get; set; }
    public bool SendTelegramMessage { get; set; }
    public bool ProjectThirdParties { get; set; }
    public bool IsActive { get; set; }
    public long CreatorId { get; set; }
    public string Creator { get; set; } = string.Empty;
    public DateTime Created { get; set; }
    public string CreatedShamsi => Created.ToShamsi();
    public long? UpdaterId { get; set; }
    public string? Updater { get; set; } = string.Empty;
    public DateTime? Updated { get; set; }
    public string? UpdatedShamsi => Updated.ToShamsi();
}