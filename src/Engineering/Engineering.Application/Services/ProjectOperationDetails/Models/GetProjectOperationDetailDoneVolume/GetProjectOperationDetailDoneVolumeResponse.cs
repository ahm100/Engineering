using Engineering.Domain.Entities.ProjectOperationDetails.Enums;

namespace Engineering.Application.Services.ProjectOperationDetails.Models.GetProjectOperationDetailDoneVolume;

public record GetProjectOperationDetailDoneVolumeResponse
{
    public long Id { get; set; }
    public decimal FinalAmount { get; set; }
    public decimal DoneAmount => DailyProjectOperations.Sum(x => x.FinalAmount);
    public decimal RemainingAmount => FinalAmount - DoneAmount;
    public List<GetDailyProjectOperationsModel> DailyProjectOperations { get; set; } = new();

};

public record GetDailyProjectOperationsModel
{
    public long Id { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public decimal FinalAmount { get; set; }
    public ProjectOperationDetailStatus Status { get; set; }
    public string StatusDescription => Status.GetEnumDescription();
    public decimal Length { get; set; }
    public decimal Width { get; set; }
    public decimal Height { get; set; }
    public decimal Weight { get; set; }
    public decimal Number { get; set; }
    public string? Description { get; set; } = string.Empty;
    public List<GetDailyDocumentsModel>? documents { get; set; }
}

public record GetDailyDocumentsModel
{
    public long Id { get; set; }
    public string Url { get; set; } = string.Empty;
}