namespace Engineering.Application.Services.RequestMachineryManagements.Models.GetRequestMachineryInquiryOperators;

public record GetRequestMachineryInquiryOperatorsResponse
{
    public long RequestMachineryId { get; set; }
    public long? RequestNumber { get; set; }
    public long? CompanyId { get; set; }
    public string? CompanyNameFa { get; set; } = string.Empty;
    public string? MachineryGroupName { get; set; } = string.Empty;
    public string? MachineryName { get; set; } = string.Empty;
    public List<GetRequestMachineryInquiryOperatorsResponseModel> Data { get; set; } = new();
    public int RowCount { get; set; }
}
