using Engineering.Domain.Entities.Projects.WBS;

namespace Engineering.Application.Abstractions.Data.Projects.WBS;

public interface IProjectScheduleColumnRepository : IBaseRepository<ProjectScheduleColumn>
{
    Task<List<ProjectScheduleColumn>> GetByImportId(
        long importId, CT ct);

    Task<ProjectScheduleColumn?> GetById(
        long id, CT ct);
}
