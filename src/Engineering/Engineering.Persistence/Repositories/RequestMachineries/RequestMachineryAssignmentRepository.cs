using Engineering.Application.Abstractions.Data.RequestMachineries;
using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Persistence.Repositories.RequestMachineries;

public class RequestMachineryAssignmentRepository : BaseRepository<EngineeringDBContext, RequestMachineryAssignment>, IRequestMachineryAssignmentRepository
{
    public RequestMachineryAssignmentRepository(EngineeringDBContext context) : base(context)
    {
    }



    public async Task<List<RequestMachineryAssignment>?> GetsAssignmentByRequestMachineryId(long id, CT ct)
    {
        var query = DbSet.Where(oo => oo.RequestMachinery.Id.Equals(id));

        var entities = await query.ToListAsync(ct);

        return entities;
    }
}
