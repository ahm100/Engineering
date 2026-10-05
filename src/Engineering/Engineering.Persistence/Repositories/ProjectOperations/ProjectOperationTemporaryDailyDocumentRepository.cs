using Engineering.Application.Abstractions.Data.ProjectOperations;
using Engineering.Domain.Entities.ProjectOperations;

namespace Engineering.Persistence.Repositories.ProjectOperations;

public class ProjectOperationTemporaryDailyDocumentRepository : BaseRepository<EngineeringDBContext, ProjectOperationTemporaryDailyDocument>, IProjectOperationTemporaryDailyDocumentRepository
{
    public ProjectOperationTemporaryDailyDocumentRepository(EngineeringDBContext context) : base(context)
    {
    }
}