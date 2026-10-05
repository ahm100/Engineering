using Engineering.Application.Abstractions.Data.EmployerContracts;
using EmployerConsideration = Engineering.Domain.Entities.EmployerContracts.EmployerConsideration;

namespace Engineering.Persistence.Repositories.EmployerContracts;

public class EmployerConsiderationRepository : BaseRepository<EngineeringDBContext, EmployerConsideration>, IEmployerConsiderationRepository
{
    public EmployerConsiderationRepository(EngineeringDBContext context) : base(context)
    {
    }

}