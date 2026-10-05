using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Application.Abstractions.Data.RequestMachineries;

public interface IRequestMachineryDocumentRepository : IBaseRepository<RequestMachineryDocument>
{
    Task<(List<RequestMachineryDocument> Data, int RowCount)> GetFiltered(long requestMachineryId, int pageIndex, int pageSize, CT ct);
    Task<List<RequestMachineryDocument>> GetDocumentsByRequestMachineryId(long requestMachineryId, CT ct);
}
