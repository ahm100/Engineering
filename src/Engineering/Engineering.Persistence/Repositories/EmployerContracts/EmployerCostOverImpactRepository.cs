using Engineering.Application.Abstractions.Data.EmployerContracts;
using Engineering.Domain.Entities.EmployerContracts;

namespace Engineering.Persistence.Repositories.EmployerContracts;

public class EmployerCostOverImpactRepository : BaseRepository<EngineeringDBContext, EmployerCostOverImpact>, IEmployerCostOverImpactRepository
{
    public EmployerCostOverImpactRepository(EngineeringDBContext context) : base(context)
    {
    }

}