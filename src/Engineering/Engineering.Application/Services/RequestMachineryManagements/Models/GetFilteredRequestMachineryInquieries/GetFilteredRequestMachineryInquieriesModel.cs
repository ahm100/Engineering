using Engineering.Domain.Entities.RequestMachineries.Enums;

namespace Engineering.Application.Services.RequestMachineryManagements.Models.GetFilteredRequestMachineryInquieries;

public record GetFilteredRequestMachineryInquieriesModel
{
    public long RequestMachineryId { get; set; }
    public long? RequestNumber { get; set; }
    public DateTime Created { get; set; }
    public string? MachineryGroupName { get; set; } = string.Empty;
    public string? MachineryName { get; set; } = string.Empty;
    public string TimeRequired { get; set; } = string.Empty;
    public RequestMachineryUnit Unit { get; set; }
    public string UnitDescription => Unit.GetEnumDescription();
    public RequestMachineryStatus Status { get; set; }
    public string StatusDescription => Status.GetEnumDescription();
    public int RequestCount { get; set; }
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public long? CreatorId { get; set; }
    public string? Creator { get; set; }
    public DateTime? ConfirmDate { get; set; }
    public long? ConfirmUserId { get; set; }
    public string? ConfirmUser { get; set; } = string.Empty;
    public string? CostCenterName { get; set; } = string.Empty;
    public string? ProjectName { get; set; } = string.Empty;
    public string? ProjectOperations { get; set; } = string.Empty;
    public string? ProjectOperationDetails { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public string? changeStatusDescription { get; set; } = string.Empty;
    public long? CompanyId { get; set; }
    public string? CompanyNameFa { get; set; } = string.Empty;
}