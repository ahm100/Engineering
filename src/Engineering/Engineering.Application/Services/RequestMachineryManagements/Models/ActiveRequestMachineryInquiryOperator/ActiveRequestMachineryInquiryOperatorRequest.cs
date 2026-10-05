namespace Engineering.Application.Services.RequestMachineryManagements.Models.ActiveRequestMachineryInquiryOperator;

public record ActiveRequestMachineryInquiryOperatorRequest(long RequestMachineryId,
                                                           long OperatorId) : IHttpRequest;
