namespace Engineering.Application.Services.RequestMachineryManagements.Models.GetRequestMachineryOperators;

public record GetRequestMachineryOperatorsResponseModel
{
    public long OperatorId { get; set; }
    public long? OperatorUserId { get; set; }
    public string? OperatorCode { get; set; } = string.Empty;
    public string? OperatorName { get; set; } = string.Empty;
    public string? DefaultPhoneNo { get; set; } = string.Empty;
    public DateTime? LastInquiryDate { get; set; }
    public int InquiryCount { get; set; }
    public int TotalInquiryCount { get; set; }
    public int InquiryCountConfirmed { get; set; }
}
