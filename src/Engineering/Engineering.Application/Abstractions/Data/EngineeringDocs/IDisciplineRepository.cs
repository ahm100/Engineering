using Engineering.Application.Services.EngineeringDocs.Contracts.GetDiscipline;
using Engineering.Domain.Entities.EngineeringDocs;

namespace Engineering.Application.Abstractions.Data.EngineeringDocs
{
    public interface IDisciplineRepository : IBaseRepository<Discipline>
    {
        Task Seeder(CancellationToken cancellationToken);

        Task<(List<GetDisciplineModel> Data, int RowCount)> GetDiscipline(
            int pageIndex,
            int pageSize,
            CT ct);

        Task<bool> IsDisciplineValid(long disciplineId, CT ct);

    }
}
