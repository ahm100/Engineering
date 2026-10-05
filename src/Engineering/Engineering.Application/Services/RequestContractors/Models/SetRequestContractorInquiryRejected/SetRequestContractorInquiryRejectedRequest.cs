namespace Engineering.Application.Services.RequestContractors.Models.SetRequestContractorInquiryRejected;

public record SetRequestContractorInquiryRejectedRequest(
    long RequestContractorId,
    string? Description) : IHttpRequest;
