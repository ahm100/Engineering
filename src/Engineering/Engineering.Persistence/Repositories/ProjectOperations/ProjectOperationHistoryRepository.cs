using Engineering.Application.Abstractions.Data.ProjectOperations;
using Engineering.Domain.Entities.ProjectOperations;

namespace Engineering.Persistence.Repositories.ProjectOperations;

public class ProjectOperationHistoryRepository : BaseRepository<EngineeringDBContext, ProjectOperationHistory>, IProjectOperationHistoryRepository
{
    public ProjectOperationHistoryRepository(EngineeringDBContext context) : base(context)
    {
    }
}