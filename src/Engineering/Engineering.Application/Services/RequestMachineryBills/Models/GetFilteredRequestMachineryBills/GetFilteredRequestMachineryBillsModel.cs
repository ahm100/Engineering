using Engineering.Application.Services.RequestMachineryBills.Models.RequestMachineryBillModel;
using Engineering.Domain.Entities.RequestMachineries.Enums;

namespace Engineering.Application.Services.RequestMachineryBills.Models.GetFilteredRequestMachineryBills;

public record GetFilteredRequestMachineryBillsModel
{
    public long RequestMachineryBillId { get; set; }
    public long? BillNumber { get; set; }
    public DateTime? BillDate { get; set; }
    public long? RequestMachineryId { get; set; }
    public long? RequestNumber { get; set; }
    public DateTime Created { get; set; }
    public long? MachineryGroupId { get; set; }
    public string? MachineryGroupName { get; set; } = string.Empty;
    public long? MachineryId { get; set; }
    public string? MachineryName { get; set; } = string.Empty;
    public RequestMachineryUnit Unit { get; set; }
    public string UnitDescription => Unit.GetEnumDescription();
    public RequestMachineryStatus Status { get; set; }
    public string StatusDescription => Status.GetEnumDescription();
    public DateTime? FromDate { get; set; }
    public TimeSpan? FromTime { get; set; }
    public DateTime? ToDate { get; set; }
    public TimeSpan? ToTime { get; set; }
    public TimeSpan? OperationDuration { get; set; }
    public decimal? UnitPrice { get; set; }
    public decimal? TotalPrice { get; set; }
    public string? ConfirmedTimeRequired { get; set; } = string.Empty;
    public string? ConfirmedDescription { get; set; } = string.Empty;
    public long? ContractorId { get; set; }
    public string? Contractor { get; set; }
    public long? SupplierId { get; set; }
    public string? Supplier { get; set; }
    public long? DriverId { get; set; }
    public string? Driver { get; set; }
    public string? DriverName { get; set; }
    public long? RequestCreatorId { get; set; }
    public string? RequestCreator { get; set; }
    public long? CreatorId { get; set; }
    public string? Creator { get; set; }
    public long? CostCenterId { get; set; }
    public string? CostCenterName { get; set; } = string.Empty;
    public long? ProjectId { get; set; }
    public string? ProjectName { get; set; } = string.Empty;
    public string? ProjectOperations { get; set; } = string.Empty;
    public string? ProjectOperationDetails { get; set; } = string.Empty;
    public long? CompanyId { get; set; }
    public string? CompanyNameFa { get; set; } = string.Empty;
    public string? NumberPlates { get; set; } = string.Empty;
    public BillNumberPlatesModel? NumberPlatesModel { get; set; } = new();
    public string? MachineryAssignment { get; set; } = string.Empty;
    public string? QRCodeUrl { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public long? BillConfirmerId { get; set; }
    public string? BillConfirmer { get; set; } = string.Empty;
}