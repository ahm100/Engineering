namespace Engineering.Application.Services.RequestMachineryManagements.Models.InActiveRequestMachineryInquiryOperator;

public record InActiveRequestMachineryInquiryOperatorRequest(long RequestMachineryId,
                                                             long OperatorId) : IHttpRequest;
