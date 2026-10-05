using Engineering.Application.Services.ProjectOperationDetails.Models.DataModels;

namespace Engineering.Application.Services.ProjectOperationDetails.Models.GetsProjectOperationDetailByProjectOperationId;

public record GetsProjectOperationDetailByProjectOperationIdResponse(
     TotalProjectOperationDetailDataModel? OtherData,
     List<ProjectOperationDetailsModel> Data,
     int RowCount
    );

public record TotalProjectOperationDetailDataModel
{
    public decimal? TotalLengths { get; set; } = 0;
    public decimal? TotalWidths { get; set; } = 0;
    public decimal? TotalHeights { get; set; } = 0;
    public decimal? TotalWeights { get; set; } = 0;
    public decimal? TotalNumbers { get; set; } = 0;
    public decimal? TotalAmounts { get; set; } = 0;
    public decimal? TotalFinalAmounts { get; set; } = 0;
    public decimal? TotalDeductionAmounts { get; set; } = 0;
    public decimal? DailyFinalAmounts { get; set; } = 0;
    public decimal? ProjectOperationWorkload { get; set; } = 0;
}