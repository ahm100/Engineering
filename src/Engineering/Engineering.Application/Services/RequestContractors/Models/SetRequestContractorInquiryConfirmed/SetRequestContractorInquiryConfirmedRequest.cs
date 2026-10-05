namespace Engineering.Application.Services.RequestContractors.Models.SetRequestContractorInquiryConfirmed;

public record SetRequestContractorInquiryConfirmedRequest(
    long RequestContractorId,
    long RequestContractorInquiryId,
    string? Description) : IHttpRequest;
