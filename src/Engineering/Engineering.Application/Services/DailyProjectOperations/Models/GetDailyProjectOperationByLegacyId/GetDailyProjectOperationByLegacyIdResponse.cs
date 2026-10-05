using Engineering.Domain.Entities.DailyProjectOperations;
using Engineering.Domain.Entities.ProjectOperationDetails.Enums;

namespace Engineering.Application.Services.DailyProjectOperations.Models.GetDailyProjectOperationByLegacyId;

public record GetDailyProjectOperationByLegacyIdResponse
{
    public long Id { get; set; }
    public long? LegacyId { get; set; }
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
}
