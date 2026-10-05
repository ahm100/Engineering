namespace Engineering.Application.Services.RequestMachineryManagements.Models.SetRequestMachineryInquiryOperator;

public record SetRequestMachineryInquiryOperatorRequest(long RequestMachineryId,
                                                        long OperatorAppoinmentId) : IHttpRequest;
