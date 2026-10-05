using Engineering.Domain.Entities.SessionRecords.Enums;

namespace Engineering.Application.Services.SessionRecords.Contracts.CreateSessionRecord;

public class CreateSessionRecordRequest : IHttpRequest
{
    public string TitleFa { get; set; } = string.Empty;
    public string? TitleEn { get; set; } = string.Empty;
    public long ProjectId { get; set;}
    public long? ContractId { get; set;}
    public string? ProjectName { get; set;}
    public long? ContractNumber { get; set; }
    public DateOnly SessionDate { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public string? Location { get; set; }
    public SessionCategory Category { get; set; }
    public SessionType Type { get; set; }

    public List<SessionInviteesModel> Invitees { get; set; } = new();
    public List<string> Items { get; set; } = new();
    public List<SessionRecordActionModel> Actions { get; set; } = new();
    public List<SessionRecordDocModel> Docs { get; set; } = new();
};

public record SessionInviteesModel(
    SessionInviteeStatus Status,
    long? CompanyId,
    long UserId);

public record SessionRecordActionModel(
    string Description,
    SessionRecordActionStatus Status,
    DateOnly DeadLine,
    long UserId);

public record SessionRecordDocModel(
    string URL,
    SessionRecordDocType Type);