using Engineering.Application.Abstractions.Data.Categories;
using Engineering.Application.Services.Categories.Models.GetCategoryByCode;

namespace Engineering.Application.Services.CabinTypes.Queries.GetCategoryByCodeForResponse;

public class GetCategoryByCodeForResponseQueryHandler : IQueryHandler<GetCategoryByCodeForResponseQuery, GetCategoryByCodeResponse?>
{
    private readonly ICategoryRepository _repository;
    private readonly ILogger<GetCategoryByCodeForResponseQueryHandler> _logger;

    public GetCategoryByCodeForResponseQueryHandler(
        ILogger<GetCategoryByCodeForResponseQueryHandler> logger,
        ICategoryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<GetCategoryByCodeResponse?>> Handle(
        GetCategoryByCodeForResponseQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetCategoryByCodeForResponse(request.CategoryCode, ct);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetCategoryByCodeResponse?>(SharedErrors.UnknownError);
        }
    }
}
