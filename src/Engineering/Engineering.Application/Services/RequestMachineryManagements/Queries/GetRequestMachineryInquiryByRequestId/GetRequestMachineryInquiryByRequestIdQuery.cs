using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Application.Services.RequestMachineryManagements.Queries.GetRequestMachineryInquiryByRequestId;

public record GetRequestMachineryInquiryByRequestIdQuery(long RequestMachineryId) : IQuery<List<RequestMachineryInquiry>>;
