using Engineering.Application.Abstractions.Data.Categories;
using Engineering.Application.Services.Categories.Models.GetsByFilterData;

namespace Engineering.Application.Services.Categories.Queries.GetsByFilterData;

public class GetsByFilterDataQueryHandler : IQueryHandler<GetsByFilterDataQuery, DataResult<List<GetsByFilterDataResponseModel>>>
{
    private readonly ICategoryRepository _repository;
    private readonly ILogger<GetsByFilterDataQueryHandler> _logger;

    public GetsByFilterDataQueryHandler(
        ILogger<GetsByFilterDataQueryHandler> logger,
        ICategoryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<GetsByFilterDataResponseModel>>?>> Handle(GetsByFilterDataQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsByFilterDataForResponse(
                request.FilterData,
                request.OrderBy,
                request.CompanyId,
                request.PageIndex,
                request.PageSize, ct);

            return result.Data.Any()
                ? new DataResult<List<GetsByFilterDataResponseModel>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                }
                : Result.Failure<DataResult<List<GetsByFilterDataResponseModel>>>(CategoryErrors.FilteredCategoryNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<GetsByFilterDataResponseModel>>>(SharedErrors.UnknownError);
        }
    }
}