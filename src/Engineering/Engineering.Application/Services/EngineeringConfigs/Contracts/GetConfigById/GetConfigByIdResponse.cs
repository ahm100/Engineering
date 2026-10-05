namespace Engineering.Application.Services.EngineeringConfigs.Contracts.GetConfigById;

public class GetConfigByIdResponse
{
    public long Id { get; set; }
    public long CompanyId { get; set; }
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