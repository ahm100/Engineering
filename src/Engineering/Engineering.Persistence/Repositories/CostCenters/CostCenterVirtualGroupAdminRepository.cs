using Engineering.Application.Abstractions.Data.CostCenters;
using Engineering.Domain.Entities.CostCenters;

namespace Engineering.Persistence.Repositories.CostCenters;

public class CostCenterVirtualGroupAdminRepository : BaseRepository<EngineeringDBContext, CostCenterVirtualGroupAdmin>, ICostCenterVirtualGroupAdminRepository
{
    public CostCenterVirtualGroupAdminRepository(EngineeringDBContext context) : base(context)
    {
    }
}
