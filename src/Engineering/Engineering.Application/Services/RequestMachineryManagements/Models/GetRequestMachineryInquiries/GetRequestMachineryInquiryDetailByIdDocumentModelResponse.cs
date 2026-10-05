namespace Engineering.Application.Services.RequestMachineryManagements.Models.GetRequestMachineryInquiries;

public record GetRequestMachineryInquiryDetailByIdDocumentModelResponse
{
    public long Id { get; set; }
    public string Url { get; set; } = string.Empty;
}
