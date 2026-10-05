using Engineering.Domain.Entities.SessionRecords.Enums;

namespace Engineering.Application.Services.SessionRecords.Contracts.GetUserSessionRecordAction;

public record GetUserSessionRecordActionResponse(
    List<GetUserSessionRecordActionResponseModel> Data,
    int RowCount);

public class GetUserSessionRecordActionResponseModel
{
    public long Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public SessionRecordActionStatus Status { get; set; }
    public string StatusDescription => Status.GetEnumDescription();
    public DateOnly Deadline { get; set; }
}