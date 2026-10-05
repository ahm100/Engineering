using Engineering.Application.Abstractions.Data.EmployerContracts;
using Engineering.Domain.Entities.EmployerContracts;

namespace Engineering.Persistence.Repositories.EmployerContracts;

public class EmployerConsiderationDepRepository : BaseRepository<EngineeringDBContext, EmployerConsiderationDep>, IEmployerConsiderationDepRepository
{

    public EmployerConsiderationDepRepository(EngineeringDBContext context) : base(context)
    {

    }

}