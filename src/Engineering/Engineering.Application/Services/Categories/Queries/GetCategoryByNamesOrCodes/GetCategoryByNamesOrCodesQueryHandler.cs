using Engineering.Application.Abstractions.Data.Categories;

namespace Engineering.Application.Services.Categories.Queries.GetCategoryByNamesOrCodes;

public class GetCategoryByNamesOrCodesQueryHandler : IQueryHandler<GetCategoryByNamesOrCodesQuery, bool>
{
    private readonly ILogger<GetCategoryByNamesOrCodesQueryHandler> _logger;
    private readonly ICategoryRepository _repository;

    public GetCategoryByNamesOrCodesQueryHandler(
        ILogger<GetCategoryByNamesOrCodesQueryHandler> logger,
        ICategoryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<bool>> Handle(GetCategoryByNamesOrCodesQuery request, CT ct)
    {
        try
        {
            var item = await _repository.GetCategoryByNamesOrCodes(
                request.Names,
                request.Codes,
                request.CompanyId, ct);
            return item;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<bool>(SharedErrors.UnknownError);
        }
    }
}