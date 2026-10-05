using Engineering.Application.Services.EngineeringDocs.Contracts.CreateProjectDocHistory;
using Engineering.Domain.Entities.EngineeringDocs;

namespace Engineering.Application.Abstractions.Data.EngineeringDocs
{
    public interface IProjectDocHistoryRepository : IBaseRepository<ProjectDocHistory>
    {
        Task<(List<GetProjectDocHistoriesModel> Data, int RowCount)> GetProjectDocHistories(
        long projectDocId, int pageIndex,
        int pageSize, CT ct);
    }
}
