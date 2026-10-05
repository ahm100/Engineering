using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Application.Abstractions.Data.RequestMachineries;

public interface IRequestMachineryAssignmentRepository : IBaseRepository<RequestMachineryAssignment>
{
    Task<List<RequestMachineryAssignment>?> GetsAssignmentByRequestMachineryId(long id, CT ct);
}
