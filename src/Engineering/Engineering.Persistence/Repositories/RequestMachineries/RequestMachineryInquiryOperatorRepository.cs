using Engineering.Application.Abstractions.Data.RequestMachineries;
using Engineering.Application.Services.RequestMachineryManagements.Queries.GetRequestMachineryInquiryOperators;
using Engineering.Application.Services.RequestMachineryManagements.Queries.GetRequestMachineryOperators;
using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Persistence.Repositories.RequestMachineries;

public class RequestMachineryInquiryOperatorRepository : BaseRepository<EngineeringDBContext, RequestMachineryInquiryOperator>, IRequestMachineryInquiryOperatorRepository
{
    public RequestMachineryInquiryOperatorRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<RequestMachineryInquiryOperator?> GetByIdAsync(long id, CT ct)
    {
        var query = DbSet.Include(oo => oo.Inquiries.Where(c => !c.IsDeleted))
                         .Where(oo => oo.Id.Equals(id));

        return await query.SingleOrDefaultAsync(ct);
    }

    public async Task<RequestMachineryInquiryOperator?> GetByMachineryAsync(long machineryId, long operatorId, CT ct)
    {
        var query = DbSet.Where(oo => oo.OperatorAppoinmentId.Equals(operatorId) && oo.RequestMachinery.Id.Equals(machineryId));

        return await query.SingleOrDefaultAsync(ct);
    }

    public async Task<List<GetRequestMachineryInquiryOperatorsQueryModel>> GetFilteredAsync(long machineryId, long machineryGroupId, CT ct)
    {
        var query = DbSet
            .OrderByDescending(oo => oo.Created)
                         .GroupBy(g => new
                         {
                             OperatorId = g.OperatorAppoinmentId
                         })
                         .Select(oo => new GetRequestMachineryInquiryOperatorsQueryModel()
                         {
                             OperatorId = oo.Key.OperatorId,
                             InquiryCount = oo.Where(c => c.RequestMachinery.Machinery!.Id == machineryId &&
                                                         c.RequestMachinery.Machinery.MachineriesGroup!.Id == machineryGroupId).Count(),
                             TotalInquiryCount = oo.Count(),
                             InquiryCountConfirmed = oo.Where(c => c.Inquiries.Any(y => y.IsConfirmed) &&
                                                                  c.RequestMachinery.Machinery!.Id == machineryId &&
                                                                  c.RequestMachinery.Machinery.MachineriesGroup!.Id == machineryGroupId).Count(),
                             LastInquiryDate = oo.OrderByDescending(p => p.Created).Select(p => p.Created).FirstOrDefault(),
                         });

        var items = await query.ToListAsync(ct);

        return items;
    }

    public async Task<List<GetRequestMachineryOperatorsQueryModel>> GetFilteredOperatorAsync(long requestMachineryId, long machineryId, long machineryGroupId, CT ct)
    {
        var query = DbSet
            .Where(x => x.RequestMachinery.Id == requestMachineryId)
            .OrderByDescending(oo => oo.Created)
                         .GroupBy(g => new
                         {
                             OperatorId = g.OperatorAppoinmentId
                         })
                         .Select(oo => new GetRequestMachineryOperatorsQueryModel()
                         {
                             OperatorId = oo.Key.OperatorId,
                             InquiryCount = oo.Where(c => c.RequestMachinery.Machinery!.Id == machineryId &&
                                                         c.RequestMachinery.Machinery.MachineriesGroup!.Id == machineryGroupId).Count(),
                             TotalInquiryCount = oo.Count(),
                             InquiryCountConfirmed = oo.Where(c => c.Inquiries.Any(y => y.IsConfirmed) &&
                                                                  c.RequestMachinery.Machinery!.Id == machineryId &&
                                                                  c.RequestMachinery.Machinery.MachineriesGroup!.Id == machineryGroupId).Count(),
                             LastInquiryDate = oo.OrderByDescending(p => p.Created).Select(p => p.Created).FirstOrDefault(),
                         });

        var items = await query.ToListAsync(ct);

        return items;
    }
}
