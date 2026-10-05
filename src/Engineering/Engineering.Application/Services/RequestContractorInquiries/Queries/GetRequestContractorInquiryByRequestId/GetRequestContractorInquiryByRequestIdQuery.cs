using Engineering.Application.Services.RequestContractorInquiries.Models.GetRequestContractorInquiryByRequestId;

namespace Engineering.Application.Services.RequestContractorInquiries.Queries.GetRequestContractorInquiryByRequestId;

public record GetRequestContractorInquiryByRequestIdQuery(
    long RequestContractorId,
    long? CompanyId,
    int PageIndex,
    int PageSize) : IQuery<DataResult<List<GetRequestContractorInquiryByRequestIdModel>>>;
