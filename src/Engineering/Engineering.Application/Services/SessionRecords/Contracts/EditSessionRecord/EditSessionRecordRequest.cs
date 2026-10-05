using Engineering.Application.Services.SessionRecords.Contracts.CreateSessionRecord;
using Engineering.Domain.Entities.SessionRecords.Enums;

namespace Engineering.Application.Services.SessionRecords.Contracts.EditSessionRecord;

public class EditSessionRecordRequest : IHttpRequest
{
    public long Id { get; set; }
    public string? TitleFa { get; set; }
    public string? TitleEn { get; set; }
    public long? ProjectId { get; set; }
    public long? ContractId { get; set; }
    public SessionCategory? Category { get; set; }
    public SessionType? Type { get; set; }
    public DateOnly? SessionDate { get; set; }
    public TimeOnly? StartTime { get; set; }
    public TimeOnly? EndTime { get; set; }

    public List<SessionRecordDocModel>? Docs { get; set; }
    public List<string>? Items { get; set; }
    public List<SessionInviteesModel>? Invitees { get; set; }
    public List<SessionRecordActionModel>? Actions { get; set; }
}