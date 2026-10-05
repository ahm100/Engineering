using Engineering.Domain.Entities.SessionRecords.Enums;

namespace Engineering.Application.Services.SessionRecords.Contracts.GetSessionRecords;

public record GetSessionRecordsResponse(
    List<GetSessionRecordsResponseModel> Data,
    int RowCount);

public class GetSessionRecordsResponseModel
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
}

