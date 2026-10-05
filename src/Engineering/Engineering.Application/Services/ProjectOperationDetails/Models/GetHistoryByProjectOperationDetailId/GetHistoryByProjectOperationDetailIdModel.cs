
using Engineering.Domain.Entities.ProjectOperationDetails.Enums;

namespace Engineering.Application.Services.ProjectOperationDetails.Models.GetHistoryByProjectOperationDetailId;

public record GetHistoryByProjectOperationDetailIdModel
{
    public long Id { get; set; }
    public string? Code { get; set; }
    public DateTime? StartDate { get; set; }
    public string? StartDateShamsi => TimeCalculator.ConvertToShamsi(StartDate);
    public DateTime? EndDate { get; set; }
    public string? EndDateShamsi => TimeCalculator.ConvertToShamsi(EndDate);
    public decimal Length { get; set; }
    public decimal Width { get; set; }
    public decimal Height { get; set; }
    public decimal Weight { get; set; }
    public decimal Number { get; set; }
    public int Day { get; set; }
    public int Hour { get; set; }
    public decimal FinalAmount { get; set; }
    public ProjectOperationDetailStatus? Status { get; set; }
    public string? StatusText => Status?.GetEnumDescription();
    public string? StatusDescription { get; set; }
    public string? Description { get; set; }
    public string? CreatDate { get; set; }
    public long CreatorId { get; set; }
    public string? Creator { get; set; }
}



