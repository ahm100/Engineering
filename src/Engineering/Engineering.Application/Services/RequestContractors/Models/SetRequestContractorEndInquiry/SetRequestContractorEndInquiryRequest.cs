namespace Engineering.Application.Services.RequestContractors.Models.SetRequestContractorEndInquiry;

public record SetRequestContractorEndInquiryRequest(
    long RequestContractorId,
    string? Description) : IHttpRequest;
