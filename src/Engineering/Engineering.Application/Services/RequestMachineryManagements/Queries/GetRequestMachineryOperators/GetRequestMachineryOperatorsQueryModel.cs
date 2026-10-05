namespace Engineering.Application.Services.RequestMachineryManagements.Queries.GetRequestMachineryOperators;

public record GetRequestMachineryOperatorsQueryModel
{
    public long OperatorId { get; set; }
    public DateTime LastInquiryDate { get; set; }
    public int InquiryCount { get; set; }
    public int TotalInquiryCount { get; set; }
    public int InquiryCountConfirmed { get; set; }
}
