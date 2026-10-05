using Engineering.Application.Services.RequestMachineries.Models.RequestMachineryBillDocumentModel;
using Engineering.Application.Services.RequestMachineries.Models.RequestMachineryDocumentModel;
using Engineering.Application.Services.RequestMachineries.Models.RequestMachineryModel;
using Engineering.Domain.Entities.RequestMachineries.Enums;

namespace Engineering.Application.Services.RequestMachineries.Models.GetRequestMachineryById;

public record GetRequestMachineryByIdResponse
{
    public long RequestMachineryId { get; set; }
    public long? RequestNumber { get; set; }
    public long? CostCenterId { get; set; }
    public string? CostCenterName { get; set; } = string.Empty;
    public long? ProjectId { get; set; }
    public string? ProjectName { get; set; } = string.Empty;
    public long? MachineriesGroupId { get; set; }
    public string? MachineriesGroupName { get; set; } = string.Empty;
    public long? MachineryId { get; set; }
    public string? MachineryName { get; set; } = string.Empty;
    public string? TimeRequired { get; set; } = string.Empty;
    public string? ConfirmTimeRequired { get; set; } = string.Empty;
    public RequestMachineryUnit Unit { get; set; }
    public string UnitDescription => Unit.GetEnumDescription();
    public RequestMachineryStatus Status { get; set; }
    public string StatusDescription => Status.GetEnumDescription();
    public int RequestCount { get; set; }
    public DateTime? FromDate { get; set; }
    public TimeSpan? FromTime { get; set; }
    public DateTime? ToDate { get; set; }
    public TimeSpan? ToTime { get; set; }
    public string? Description { get; set; }
    public DateTime? ConfirmFromDate { get; set; }
    public TimeSpan? ConfirmFromTime { get; set; }
    public DateTime? ConfirmToDate { get; set; }
    public TimeSpan? ConfirmToTime { get; set; }
    public string? MachineryIdentifier { get; set; } = string.Empty;
    public RequestNumberPlatesModel? BillNumberPlatesModel { get; set; } = new();
    public DateTime? ConfirmDate { get; set; }
    public long? ConfirmUserId { get; set; }
    public string? ConfirmUser { get; set; } = string.Empty;
    public long? CreatorId { get; set; }
    public string? Creator { get; set; } = string.Empty;
    public List<ProjectOperationModel>? ProjectOperations { get; set; } = new();
    public List<ProjectOperationDetailModel>? ProjectOperationDetails { get; set; } = new();
    public List<RequestMachineryDocumentResponseModel>? RequestMachineryDocuments { get; set; } = new();
    public List<RequestMachineryBillDocumentResponseModel>? RequestMachineryBillDocuments { get; set; } = new();
    public long? CompanyId { get; set; }
    public string? CompanyNameFa { get; set; } = string.Empty;
    public long? ContractorId { get; set; }
    public string? Contractor { get; set; } = string.Empty;
    public long? DriverId { get; set; }
    public string? Driver { get; set; } = string.Empty;
    public string? DriverName { get; set; } = string.Empty;
    public TimeSpan? OperationWork { get; set; }
    public decimal? UnitPrice { get; set; }
    public decimal? TotalPrice { get; set; }
    public long? OperationContractorId { get; set; }
    public string? OperationContractor { get; set; }
}
