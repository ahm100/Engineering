using Engineering.Domain.Entities.SessionRecords.Enums;

namespace Engineering.Application.Services.SessionRecords.Contracts.GetSessionRecordDetail;

public class GetSessionRecordDetailResponse
{
    public long Id { get; set; }
    public string? TitleEn { get; set; }
    public string TitleFa { get; set; } = string.Empty;
    public SessionCategory Category { get; set; }
    public string CategoryDescription => Category.GetEnumDescription();
    public SessionType Type { get; set; }
    public string TypeDescription => Type.GetEnumDescription();
    public string? ProjectName { get; set; }
    public long? ContractNumber { get; set; }
    public long? ContractId { get; set; }
    public long? ProjectId { get; set; }
    public string? Location { get; set; }
    public DateOnly SessionDate { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public List<SessionInviteeReadModel>? Invitees { get; set; }
    public List<SessionRecordDocReadModel>? Docs { get; set; }
    public List<SessionRecordActionReadModel>? Actions { get; set; }
    public List<SessionItemReadModel>? Items { get; set; }
}

public class SessionInviteeReadModel
{
    public long Id { get; set; }
    public long? UserId { get; set; }
    public SessionInviteeStatus Status { get; set; }
    public string StatusDescription => Status.GetEnumDescription();
    public long? CompanyId { get; set; }
    public string? CompanyName { get; set; }
    public string? UserFullName { get; set; }
    public string? PostTitle { get; set; }
};

public record SessionRecordDocReadModel(
    long Id,
    SessionRecordDocType Type,
    string TypeDescription,
    string URL);

public class SessionRecordActionReadModel
{
    public long Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public SessionRecordActionStatus Status { get; set; }
    public string StatusDescription => Status.GetEnumDescription();
    public DateOnly DeadLine { get; set; }
    public long? UserId { get; set; }
    public string? UserFullName { get; set; }
}

public record SessionItemReadModel(
    long Id,
    string Description);