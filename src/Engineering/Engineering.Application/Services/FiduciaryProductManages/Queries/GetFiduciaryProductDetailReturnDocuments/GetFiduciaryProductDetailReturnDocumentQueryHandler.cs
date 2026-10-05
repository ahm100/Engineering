using Engineering.Application.Abstractions.Data.FiduciaryProducts;
using Engineering.Domain.Entities.FiduciaryProducts;

namespace Engineering.Application.Services.FiduciaryProductManages.Queries.GetFiduciaryProductDetailReturnDocuments;

public class GetFiduciaryProductDetailReturnDocumentQueryHandler : IQueryHandler<GetFiduciaryProductDetailReturnDocumentQuery, DataResult<List<FiduciaryProductDetailReturnDocument>>>
{
    private readonly ILogger<GetFiduciaryProductDetailReturnDocumentQueryHandler> _logger;
    private readonly IFiduciaryProductDetailReturnDocumentRepository _repository;

    public GetFiduciaryProductDetailReturnDocumentQueryHandler(ILogger<GetFiduciaryProductDetailReturnDocumentQueryHandler> logger, IFiduciaryProductDetailReturnDocumentRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<FiduciaryProductDetailReturnDocument>>?>> Handle(GetFiduciaryProductDetailReturnDocumentQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetFiltered(request.FiduciaryProductDetailReturnId, request.OrderBy, request.PageIndex, request.PageSize, ct);
            return result.Data.Any() ?
                      new DataResult<List<FiduciaryProductDetailReturnDocument>>
                      {
                          Data = result.Data,
                          RowCount = result.RowCount
                      } : Result.Failure<DataResult<List<FiduciaryProductDetailReturnDocument>>>(FiduciaryProductDetailReturnDocumentErrors.FiduciaryProductDetailReturnDocumentWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<FiduciaryProductDetailReturnDocument>>>(SharedErrors.UnknownError);
        }
    }
}
