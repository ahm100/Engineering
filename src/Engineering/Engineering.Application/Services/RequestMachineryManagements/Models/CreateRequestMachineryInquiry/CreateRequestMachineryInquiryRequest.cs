namespace Engineering.Application.Services.RequestMachineryManagements.Models.CreateRequestMachineryInquiry;

public record CreateRequestMachineryInquiryRequest(long RequestMachineryId,
                                                   bool EndInquiry,
                                                   List<CreateRequestMachineryInquiryRequestModel> Inquiries) : IHttpRequest;
