using Engineering.Application.Abstractions.Data.Categories;
using Category = Engineering.Domain.Entities.Categories.Category;

namespace Engineering.Application.Services.Categories.Commands.ActiveCategory;

public class ActiveCategoryCommandHandler : ICommandHandler<ActiveCategoryCommand, Category>
{
    private readonly ILogger<ActiveCategoryCommand> _logger;
    private readonly ICategoryRepository _repository;

    public ActiveCategoryCommandHandler(
        ILogger<ActiveCategoryCommand> logger,
        ICategoryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<Category?>> Handle(ActiveCategoryCommand request, CT ct)
    {
        try
        {
            var entity = request.Entity;
            if (entity is null)
                return Result.Failure<Category>(CategoryErrors.CategoryWithIdNotFound);
            if (entity.IsActive)
                return Result.Failure<Category>(CategoryErrors.IsActive);

            entity.SetActive();
            await _repository.Update(entity);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<Category>(SharedErrors.UnknownError);
        }
    }
}