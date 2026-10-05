using Engineering.Domain.Entities.FiduciaryProducts;

namespace Engineering.Application.Services.FiduciaryProductManages.Queries.GetFiduciaryProductDetailReturnDocuments;

public record GetFiduciaryProductDetailReturnDocumentQuery(long FiduciaryProductDetailReturnId,
                                                           string[]? OrderBy,
                                                           int PageIndex,
                                                           int PageSize) : IQuery<DataResult<List<FiduciaryProductDetailReturnDocument>>>;
