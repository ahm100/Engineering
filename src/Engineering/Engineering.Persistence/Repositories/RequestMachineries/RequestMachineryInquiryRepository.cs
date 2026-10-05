using Engineering.Application.Abstractions.Data.RequestMachineries;
using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Persistence.Repositories.RequestMachineries;

public class RequestMachineryInquiryRepository : BaseRepository<EngineeringDBContext, RequestMachineryInquiry>, IRequestMachineryInquiryRepository
{
    public RequestMachineryInquiryRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<RequestMachineryInquiry?> GetByIdAsync(long id, long requestId, CT ct)
    {
        var query = DbSet.Where(oo =>
                             oo.RequestMachineryInquiryOperator.IsActive &&
                             oo.RequestMachineryInquiryOperator.RequestMachinery.Id.Equals(requestId) &&
                             oo.Id.Equals(id))
            .OrderByDescending(oo => oo.Created);

        return await query.SingleOrDefaultAsync(ct);
    }

    public async Task<List<RequestMachineryInquiry>?> GetsInquiryByRequestMachineryId(long id, CT ct)
    {
        var query = DbSet.Where(oo => oo.RequestMachineryInquiryOperator.RequestMachinery.Id.Equals(id));

        var entities = await query.ToListAsync(ct);

        return entities;
    }

    public async Task<List<RequestMachineryInquiry>> GetByRequestIdAsync(long requestId, CT ct)
    {
        var query = DbSet.Where(oo =>
                                     oo.RequestMachineryInquiryOperator.IsActive &&
                                     oo.RequestMachineryInquiryOperator.RequestMachinery.Id.Equals(requestId))
            .OrderByDescending(oo => oo.Created);

        return await query.ToListAsync(ct);
    }
}
