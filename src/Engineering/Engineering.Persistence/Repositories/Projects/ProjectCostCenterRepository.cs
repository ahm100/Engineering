using Engineering.Application.Abstractions.Data.Projects;
using Engineering.Domain.Entities.Projects.Junctions;

namespace Engineering.Persistence.Repositories.Projects;

public class ProjectCostCenterRepository : BaseRepository<EngineeringDBContext, ProjectCostCenter>, IProjectCostCenterRepository
{
    public ProjectCostCenterRepository(EngineeringDBContext context) : base(context)
    {
    }
}