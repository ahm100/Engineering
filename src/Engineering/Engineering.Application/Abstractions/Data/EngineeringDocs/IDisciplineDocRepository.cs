using Engineering.Application.Services.EngineeringDocs.Contracts.GetDisciplineDoc;
using Engineering.Domain.Entities.EngineeringDocs;

namespace Engineering.Application.Abstractions.Data.EngineeringDocs
{
    public interface IDisciplineDocRepository : IBaseRepository<DisciplineDoc>
    {
        Task Seeder(CancellationToken cancellationToken);


        Task<(List<GetDisciplineDocModel> Data, int RowCount)> GetDisciplineDoc(
            int pageIndex,
            int pageSize,
            CT ct);

        Task<bool> IsDisciplineDocValid(long disciplineDocId, CT ct);
    }
}
