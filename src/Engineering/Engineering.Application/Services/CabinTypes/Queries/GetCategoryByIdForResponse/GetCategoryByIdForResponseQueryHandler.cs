using Engineering.Application.Abstractions.Data.Categories;
using Engineering.Application.Services.Categories.Models.GetCategoryById;
using Engineering.Application.Services.Categories.Queries.GetCategoryByIdForResponse;

public class GetCategoryByIdForResponseQueryHandler : IQueryHandler<GetCategoryByIdForResponseQuery, GetCategoryByIdResponse>
{
    private readonly ICategoryRepository _repository;
    private readonly ILogger<GetCategoryByIdForResponseQueryHandler> _logger;

    public GetCategoryByIdForResponseQueryHandler(
        ILogger<GetCategoryByIdForResponseQueryHandler> logger,
        ICategoryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<GetCategoryByIdResponse?>> Handle(
        GetCategoryByIdForResponseQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetCategoryByIdForResponse(request.Id, ct);
            return result ?? Result.Failure<GetCategoryByIdResponse?>(CategoryErrors.CategoryWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetCategoryByIdResponse?>(SharedErrors.UnknownError);
        }
    }
}