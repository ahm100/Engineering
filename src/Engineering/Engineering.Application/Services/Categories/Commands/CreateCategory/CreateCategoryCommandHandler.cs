using Engineering.Application.Abstractions.Data.Categories;
using Engineering.Application.Services.Categories.Models.CreateCategory;
using Category = Engineering.Domain.Entities.Categories.Category;

namespace Engineering.Application.Services.Categories.Commands.CreateCategory;

public class CreateCategoryCommandHandler : ICommandHandler<CreateCategoryCommand, CreateCategoryResponse?>
{
    private readonly ILogger<CreateCategoryCommand> _logger;
    private readonly ICategoryRepository _repository;

    public CreateCategoryCommandHandler(
        ILogger<CreateCategoryCommand> logger,
        ICategoryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<CreateCategoryResponse?>> Handle(CreateCategoryCommand request, CT ct)
    {
        try
        {
            var entity = new Category(
                request.CategoryName,
                request.CategoryCode,
                request.IsActive,
                request.CompanyId);
            var result = await _repository.Create(entity, ct);

            return new CreateCategoryResponse(
                result.Id, result.CategoryCode, result.CategoryName, result.IsActive);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<CreateCategoryResponse?>(SharedErrors.UnknownError);
        }
    }
}