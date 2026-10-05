using Engineering.Application.Services.RequestContractorInquiries.Models.GetRequestContractorInquiryByRequestId;
using Engineering.Application.Services.RequestContractors.Models.GetRequestContractorInquiryById;
using Engineering.Domain.Entities.RequestContractors;

namespace Engineering.Application.Abstractions.Data.RequestContractors;

public interface IRequestContractorInquiryRepository : IBaseRepository<RequestContractorInquiry>
{
    Task<RequestContractorInquiry?> GetById(long id, CT ct);

    Task<GetRequestContractorInquiryByIdResponse?> GetInquiryModelById(
        long id,
        CT ct);

    Task<(List<GetRequestContractorInquiryByRequestIdModel> Data, int RowCount)> GetsInquiryByRequestContractorId(
        long id,
        long? companyId,
        int pageIndex,
        int pageSize,
        CT ct);
}
