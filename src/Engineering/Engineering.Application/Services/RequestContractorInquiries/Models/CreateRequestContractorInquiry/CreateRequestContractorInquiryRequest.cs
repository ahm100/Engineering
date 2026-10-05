namespace Engineering.Application.Services.RequestContractorInquiries.Models.CreateRequestContractorInquiry;

public record CreateRequestContractorInquiryRequest(
    long RequestContractorId,
    List<CreateRequestContractorInquiryRequestModel> Inquiries) : IHttpRequest;
