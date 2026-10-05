using Engineering.Domain.Entities.RequestMachineries.Enums;

namespace Engineering.Application.Services.RequestMachineries.Models.GetOnProjectRequestReports;

public record GetOnProjectRequestReportsModel
{
    public long RequestMachineryId { get; set; }
    public long? RequestNumber { get; set; }
    public long? CostCenterId { get; set; }
    public string? CostCenterName { get; set; } = string.Empty;
    public long? ProjectId { get; set; }
    public string? ProjectName { get; set; } = string.Empty;
    public List<long>? ProjectOperationIds { get; set; }
    public string? ProjectOperations { get; set; } = string.Empty;
    public List<long>? ProjectOperationDetailIds { get; set; }
    public string? ProjectOperationDetails { get; set; } = string.Empty;
    public DateTime Created { get; set; }
    public string? MachineryGroupName { get; set; } = string.Empty;
    public long? MachineryId { get; set; }
    public string? MachineryName { get; set; } = string.Empty;
    public string? MachineryCode { get; set; } = string.Empty;
    public string? TimeRequired { get; set; } = string.Empty;
    public RequestMachineryUnit Unit { get; set; }
    public string UnitDescription => Unit.GetEnumDescription();
    public RequestMachineryStatus Status { get; set; }
    public string StatusDescription => Status.GetEnumDescription();
    public int RequestCount { get; set; }
    public decimal? TotalPrice { get; set; } = 0;
    public decimal? UnitPrice { get; set; } = 0;
    public long? CurrencyId { get; set; }
    public string? Currency { get; set; }
    public long? ContractorId { get; set; }
    public string? Contractor { get; set; }
    public long? OperatorId { get; set; }
    public long? OperatorUserId { get; set; }
    public string? Operator { get; set; }
    public DateTime? FromDate { get; set; }
    public TimeSpan? FromTime { get; set; }
    public DateTime? ToDate { get; set; }
    public TimeSpan? ToTime { get; set; }
    public DateTime? ConfirmFromDate { get; set; }
    public TimeSpan? ConfirmFromTime { get; set; }
    public DateTime? ConfirmToDate { get; set; }
    public TimeSpan? ConfirmToTime { get; set; }
    public long? ConfirmUserId { get; set; }
    public string? ConfirmUser { get; set; } = string.Empty;
    public DateTime? ConfirmDate { get; set; }
    public long? CreatorId { get; set; }
    public string? Creator { get; set; }
    public long? CompanyId { get; set; }
    public string? Company { get; set; }
    public long? DriverId { get; set; }
    public string? Driver { get; set; }
    public string? DriverName { get; set; }
    public string? ConfirmedTimeRequired { get; set; }
    public string? ConfirmedDescription { get; set; }
    public RequestMachineryPaymentType? PaymentType { get; set; }
    public string? PaymentTypeTitle => PaymentType?.GetEnumDescription();
}