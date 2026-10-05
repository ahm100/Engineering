namespace Engineering.Application.Services.RequestMachineryManagements.Models.SetRequestMachineryInquiryDone;

public record SetRequestMachineryInquiryDoneRequest(long RequestMachineryId,
                                                    long RequestMachineryInquiryId,
                                                    string? Description) : IHttpRequest;
