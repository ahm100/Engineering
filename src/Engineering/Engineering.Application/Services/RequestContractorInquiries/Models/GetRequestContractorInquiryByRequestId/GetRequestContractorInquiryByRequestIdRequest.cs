namespace Engineering.Application.Services.RequestContractorInquiries.Models.GetRequestContractorInquiryByRequestId;

public record GetRequestContractorInquiryByRequestIdRequest(
    long RequestContractorId,
    int PageIndex,
    int PageSize) : IHttpRequest;
