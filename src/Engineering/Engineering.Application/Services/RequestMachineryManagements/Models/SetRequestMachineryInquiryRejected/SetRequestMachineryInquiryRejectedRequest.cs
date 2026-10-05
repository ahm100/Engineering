namespace Engineering.Application.Services.RequestMachineryManagements.Models.SetRequestMachineryInquiryRejected;

public record SetRequestMachineryInquiryRejectedRequest(long RequestMachineryId,
                                                  string? Description) : IHttpRequest;
