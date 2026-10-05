using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Application.Abstractions.Data.RequestMachineries;

public interface IRequestMachineryInquiryRepository : IBaseRepository<RequestMachineryInquiry>
{
    Task<List<RequestMachineryInquiry>> GetByRequestIdAsync(long requestId, CT ct);

    Task<RequestMachineryInquiry?> GetByIdAsync(long id, long requestId, CT ct);

    Task<List<RequestMachineryInquiry>?> GetsInquiryByRequestMachineryId(long id, CT ct);
}
