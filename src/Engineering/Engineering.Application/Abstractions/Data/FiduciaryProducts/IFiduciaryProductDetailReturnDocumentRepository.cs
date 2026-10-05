using Engineering.Domain.Entities.FiduciaryProducts;

namespace Engineering.Application.Abstractions.Data.FiduciaryProducts;

public interface IFiduciaryProductDetailReturnDocumentRepository : IBaseRepository<FiduciaryProductDetailReturnDocument>
{
    Task<(List<FiduciaryProductDetailReturnDocument> Data, int RowCount)> GetFiltered(long fiduciaryProductDetailReturnId, string[]? orderBy, int pageIndex, int pageSize, CT ct);
}
