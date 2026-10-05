using Engineering.Domain.Entities.DailyProjectOperations;
using Engineering.Domain.Entities.ProjectOperationDetails.Enums;

namespace Engineering.Application.Services.DailyProjectOperations.Models.GetDailyProjectOperationHistoryById;

public record GetDailyProjectOperationHistoryByIdResponse
(
    List<GetDailyProjectOperationHistoryByIdModel> Data,
    int RowCount
    );

public record GetDailyProjectOperationHistoryByIdModel
{
    public long Id { get; set; }
    public ProjectOperationDetailStatus Status { get; set; }
    public string StatusDescription => Status.GetEnumDescription();
    public DailyProjectOperationType Type { get; set; }
    public string? TypeDescription => Type.GetEnumDescription();
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal Length { get; set; }
    public decimal Width { get; set; }
    public decimal Height { get; set; }
    public decimal Weight { get; set; }
    public decimal Number { get; set; }
    public decimal FinalAmount => Length * Width * Height * Weight * Number;
    public string? Description { get; set; } = string.Empty;
    public long CreatorId { get; set; }
    public string? CreatorName { get; set; } = string.Empty;
    public DateTime Created { get; set; }
    public bool IsDeleted { get; set; }
}