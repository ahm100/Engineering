
using Engineering.Domain.Entities.RequestMachineries.Enums;

namespace Engineering.Application.Services.RequestMachineryManagements.Models.GetsRequestMachineryManagementExcelExporter;

public record GetsRequestMachineryManagementExcelExporterResponseModel
{
    public long Id { get; set; }
    public long? RequestNumber { get; set; }
    public string Created { get; set; } = string.Empty;
    public string? MachineryGroupName { get; set; } = string.Empty;
    public string? MachineryName { get; set; } = string.Empty;
    public string TimeRequired { get; set; } = string.Empty;
    public string UnitDescription { get; set; } = string.Empty;
    public string StatusDescription { get; set; } = string.Empty;
    public int RequestCount { get; set; }
    public string? FromDate { get; set; } = string.Empty;
    public TimeSpan? FromTime { get; set; }
    public string? ToDate { get; set; } = string.Empty;
    public TimeSpan? ToTime { get; set; }
    public long? CreatorId { get; set; }
    public string? Creator { get; set; }
    public string? CostCenterName { get; set; } = string.Empty;
    public string? ProjectName { get; set; } = string.Empty;
    public string? ProjectOperations { get; set; } = string.Empty;
    public string? ProjectOperationDetails { get; set; } = string.Empty;
    public string? ConfirmDate { get; set; } = string.Empty;
    public long? ConfirmUserId { get; set; }
    public string? ConfirmUser { get; set; }
    public long? AppointmentId { get; set; }
    public string? AppointmentFullName { get; set; }
    public long? CompanyId { get; set; }
    public string? CompanyNameFa { get; set; }
    public string? ConfirmFromDate { get; set; }
    public TimeSpan? ConfirmFromTime { get; set; }
    public string? ConfirmToDate { get; set; }
    public TimeSpan? ConfirmToTime { get; set; }
    public string? changeStatusDescription { get; set; } = string.Empty;
    public string? ConfirmedTimeRequired { get; set; } = string.Empty;
    public string? ConfirmedDescription { get; set; } = string.Empty;
    public long? ContractorId { get; set; }
    public string? Contractor { get; set; }
    public decimal? UnitPrice { get; set; }
    public decimal? TotalPrice { get; set; }
    public RequestMachineryPaymentType? PaymentType { get; set; }
    public string? PaymentTypeTitle => PaymentType?.GetEnumDescription();
}