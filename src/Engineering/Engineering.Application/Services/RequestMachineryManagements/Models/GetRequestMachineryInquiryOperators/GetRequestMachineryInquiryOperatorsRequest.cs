namespace Engineering.Application.Services.RequestMachineryManagements.Models.GetRequestMachineryInquiryOperators;

public record GetRequestMachineryInquiryOperatorsRequest(long RequestMachineryId,
                                                         string? FilterData,
                                                         int PageIndex,
                                                         int PageSize) : IHttpRequest;
