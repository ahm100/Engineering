using Engineering.Application.Services.EngineeringDocs.Contracts.GetProjectDocById;
using Engineering.Application.Services.EngineeringDocs.Contracts.GetProjectDocs;
using Engineering.Domain.Entities.EngineeringDocs;

namespace Engineering.Application.Abstractions.Data.EngineeringDocs
{
    public interface IProjectDocRepository : IBaseRepository<ProjectDoc>
    {

        Task<(string ProjectCode, string DisciplineCode, string DisciplineDocCode)?>
        GetProjectDocCodeData(
        long projectId,
        long disciplineId,
        long disciplineDocId,
        CT ct);

        Task<int> GetNextSequence(
           long projectId,
           long disciplineId,
           long disciplineDocId,
           CT ct);


        Task<GetProjectDocByIdResponse?> GetProjectDocById(
         long id,
         CT ct);

        Task<(List<GetProjectDocsModel> Data, int RowCount)> GetProjectDocs(
            long? projectId,
            long? disciplineId,
            long? disciplineDocId,
            int pageIndex,
            int pageSize,
            CT ct);

        Task<ProjectDoc?> GetProjectDocEntityById(
        long id, CT ct);

        //
    }
}
