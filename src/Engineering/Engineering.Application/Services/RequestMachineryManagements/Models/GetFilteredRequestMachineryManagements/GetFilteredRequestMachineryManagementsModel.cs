using Engineering.Application.Services.RequestMachineries.Models.RequestMachineryBillDocumentModel;
using Engineering.Application.Services.RequestMachineries.Models.RequestMachineryDocumentModel;
using Engineering.Application.Services.RequestMachineryManagements.Models.GetRequestMachineryManagementById;
using Engineering.Domain.Entities.RequestMachineries.Enums;

namespace Engineering.Application.Services.RequestMachineryManagements.Models.GetFilteredRequestMachineryManagements;

public record GetFilteredRequestMachineryManagementsModel
{
    public long RequestMachineryId { get; set; }
    public long? RequestNumber { get; set; }
    public DateTime Created { get; set; }
    public string? MachineryGroupName { get; set; } = string.Empty;
    public string? MachineryName { get; set; } = string.Empty;
    public string? TimeRequired { get; set; } = string.Empty;
    public RequestMachineryUnit Unit { get; set; }
    public string UnitDescription => Unit.GetEnumDescription();
    public RequestMachineryStatus Status { get; set; }
    public string StatusDescription => Status.GetEnumDescription();
    public int RequestCount { get; set; }
    public DateTime? FromDate { get; set; }
    public string? FromDateShamsi => TimeCalculator.ConvertToShamsi(FromDate);
    public TimeSpan? FromTime { get; set; }
    public DateTime? ToDate { get; set; }
    public string? ToDateShamsi => TimeCalculator.ConvertToShamsi(ToDate);
    public TimeSpan? ToTime { get; set; }
    public long? CreatorId { get; set; }
    public string? Creator { get; set; }
    public string? CostCenterName { get; set; } = string.Empty;
    public string? ProjectName { get; set; } = string.Empty;
    public string? ProjectOperations { get; set; } = string.Empty;
    public string? ProjectOperationDetails { get; set; } = string.Empty;
    public DateTime? ConfirmDate { get; set; }
    public string? ConfirmDateShamsi => TimeCalculator.ConvertToShamsi(ConfirmDate);
    public long? ConfirmUserId { get; set; }
    public string? ConfirmUser { get; set; }
    public long? CompanyId { get; set; }
    public string? CompanyNameFa { get; set; } = string.Empty;
    public string? changeStatusDescription { get; set; } = string.Empty;
    public DateTime? ConfirmFromDate { get; set; }
    public string? ConfirmFromDateShamsi => TimeCalculator.ConvertToShamsi(ConfirmFromDate);
    public TimeSpan? ConfirmFromTime { get; set; }
    public DateTime? ConfirmToDate { get; set; }
    public string? ConfirmToDateShamsi => TimeCalculator.ConvertToShamsi(ConfirmToDate);
    public TimeSpan? ConfirmToTime { get; set; }
    public string? ConfirmedTimeRequired { get; set; } = string.Empty;
    public string? ConfirmedDescription { get; set; } = string.Empty;
    public long? ContractorId { get; set; }
    public string? Contractor { get; set; }
    public long? DriverId { get; set; }
    public string? Driver { get; set; }
    public string? DriverName { get; set; }
    public string? Description { get; set; }
    public decimal? TotalPrice { get; set; }
    public decimal? UnitPrice { get; set; }
    public RequestMachineryPaymentType? PaymentType { get; set; }
    public string? PaymentTypeTitle => PaymentType?.GetEnumDescription();
    public GetRequestMachineryManagementByIdOperatorModel? InquiryOperator { get; set; } = new();
    public List<RequestMachineryDocumentResponseModel>? RequestMachineryDocuments { get; set; } = new();
    public List<RequestMachineryBillDocumentResponseModel>? RequestMachineryBillDocuments { get; set; } = new();
}