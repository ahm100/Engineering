using Engineering.Domain.Entities.RequestMachineries.Enums;

namespace Engineering.Application.Services.RequestMachineryManagements.Models.GetRequestMachineryInquiries;

public record GetRequestMachineryInquiriesResponse
{
    public long? RequestNumber { get; set; }
    public string? CostCenterName { get; set; } = string.Empty;
    public string? ProjectName { get; set; } = string.Empty;
    public string? MachineryName { get; set; } = string.Empty;
    public string? MachineryGroupName { get; set; } = string.Empty;
    public string TimeRequired { get; set; } = string.Empty;
    public RequestMachineryUnit Unit { get; set; }
    public string UnitDescription => Unit.GetEnumDescription();
    public RequestMachineryStatus Status { get; set; }
    public string StatusDescription => Status.GetEnumDescription();
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public int RequestCount { get; set; }
    public string? Description { get; set; }
    public long? CompanyId { get; set; }
    public string? CompanyNameFa { get; set; } = string.Empty;
    public List<GetRequestMachineryInquiriesResponseModel>? Data { get; set; } = new();
}
