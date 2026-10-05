
using Engineering.Domain.Entities.RequestMachineries.Enums;

namespace Engineering.Application.Services.RequestMachineries.Models.GetOnProjectRequestReportsExcelExporter;

public record GetOnProjectRequestReportsExcelExporterModel
{
    public long RequestMachineryId { get; set; }
    public long? RequestNumber { get; set; }
    public string? CostCenterName { get; set; } = string.Empty;
    public string? ProjectName { get; set; } = string.Empty;
    public string? ProjectOperations { get; set; } = string.Empty;
    public string? ProjectOperationDetails { get; set; } = string.Empty;
    public DateTime Created { get; set; }
    public string? MachineryGroupName { get; set; } = string.Empty;
    public string? MachineryName { get; set; } = string.Empty;
    public string? MachineryCode { get; set; } = string.Empty;
    public string? TimeRequired { get; set; } = string.Empty;
    public RequestMachineryUnit Unit { get; set; }
    public string UnitDescription => Unit.GetEnumDescription();
    public RequestMachineryStatus Status { get; set; }
    public string StatusDescription => Status.GetEnumDescription();
    public int RequestCount { get; set; }
    public decimal? TotalPrice { get; set; } = 0;
    public string? Currency { get; set; }
    public string? Contractor { get; set; }
    public string? Operator { get; set; }
    public DateTime? FromDate { get; set; }
    public string? FromTimee => FromDate?.TimeOfDay.ToString() ?? TimeSpan.Zero.ToString();
    public DateTime? ToDate { get; set; }
    public string? ToTimee => ToDate?.TimeOfDay.ToString() ?? TimeSpan.Zero.ToString();
    public DateTime? ConfirmFromDate { get; set; }
    public string? ConfirmFromTimee => ConfirmFromDate?.TimeOfDay.ToString() ?? TimeSpan.Zero.ToString();
    public DateTime? ConfirmToDate { get; set; }
    public string? ConfirmToTimee => ConfirmToDate?.TimeOfDay.ToString() ?? TimeSpan.Zero.ToString();
    public string? ConfirmUser { get; set; } = string.Empty;
    public DateTime? ConfirmDate { get; set; }
    public string? Creator { get; set; }
    public string? Company { get; set; }
    public string? Driver { get; set; }
    public string? DriverName { get; set; }
    public string? ConfirmedTimeRequired { get; set; }
    public string? ConfirmedDescription { get; set; }
    public decimal? UnitPrice { get; set; }
    public RequestMachineryPaymentType? PaymentType { get; set; }
    public string? PaymentTypeTitle => PaymentType?.GetEnumDescription();
}