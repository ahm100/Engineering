using Engineering.Domain.Entities.ProjectOperationDetails.Enums;

namespace Engineering.Application.Services.DailyProjectOperations.Models.GetDailyHistoriesExcelExporter;

public record GetDailyHistoriesExcelExporterResponseModel
{
    public long DailyProjectOperationId { get; set; }
    public long? ProjectOperationDetailId { get; set; }
    public string? PublicName { get; set; } = string.Empty;
    public long? ProjectOperationId { get; set; }
    public string? OperationInfoName { get; set; } = string.Empty;
    public long? ProjectId { get; set; }
    public string? ProjectName { get; set; } = string.Empty;
    public long? CostCenterId { get; set; }
    public string? CostCenterName { get; set; } = string.Empty;
    public DateTime StartDateMiladi { get; set; }
    public string StartDate => StartDateMiladi.ToShamsi();
    public string? EndDate { get; set; }
    public ProjectOperationDetailStatus Status { get; set; }
    public string StatusDescription => Status.GetEnumDescription();
    public decimal FinalAmount { get; set; }
    public long? UnitOfMeasurementId { get; set; }
    public string? UnitOfMeasurement { get; set; } = string.Empty;
    public DateTime CreatedMiladi { get; set; }
    public string Created => CreatedMiladi.ToShamsi();
    public long? CompanyId { get; set; }
    public string? CompanyNameFa { get; set; } = string.Empty;
    public string? PublicCode { get; set; } = string.Empty;
    public string? PrivateName { get; set; } = string.Empty;
    public string? PrivateCode { get; set; } = string.Empty;
    public string? ProjectOperationDetailDescription { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
}
public record GetTotalsByProjectOperationDetailIdExcelExporterResponse
{
    public decimal? TotalLengths { get; set; } = 0;
    public decimal? TotalWidths { get; set; } = 0;
    public decimal? TotalHeights { get; set; } = 0;
    public decimal? TotalWeights { get; set; } = 0;
    public decimal? TotalNumbers { get; set; } = 0;
    public decimal? TotalAmounts { get; set; } = 0;
    public decimal? ProjectOperationDetailFinalAmount { get; set; } = 0;
    public decimal? TotalDeductionFinalAmount { get; set; } = 0;
    public decimal? TotalProjectOperationDetailFinalAmount { get; set; } = 0;
    public decimal? ProjectOperationWorkload { get; set; } = 0;
}
