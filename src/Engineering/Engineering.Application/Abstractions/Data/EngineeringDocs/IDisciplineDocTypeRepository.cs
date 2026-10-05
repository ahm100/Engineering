using Engineering.Application.Services.EngineeringDocs.Contracts.GetDisciplineDocType;
using Engineering.Domain.Entities.EngineeringDocs;

namespace Engineering.Application.Abstractions.Data.EngineeringDocs
{
    public interface IDisciplineDocTypeRepository : IBaseRepository<DisciplineDocType>
    {
        Task SeedEngineeringDocs(CancellationToken cancellationToken);

        Task<(List<GetDisciplineDocTypeModel> Data, int RowCount)> GetDisciplineDocType(
            long? disciplineId,
            int pageIndex,
            int pageSize,
            CT ct);
        Task<bool> IsDisciplineDocTypeValid(
        long disciplineId, long disciplineDocId, CT ct);

    }
}
