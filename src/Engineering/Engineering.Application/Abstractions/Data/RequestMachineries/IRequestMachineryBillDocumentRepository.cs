using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Application.Abstractions.Data.RequestMachineries;

public interface IRequestMachineryBillDocumentRepository : IBaseRepository<RequestMachineryBillDocument>
{
    Task<(List<RequestMachineryBillDocument> Data, int RowCount)> GetFiltered(long requestMachineryId, int pageIndex, int pageSize, CT ct);
}
