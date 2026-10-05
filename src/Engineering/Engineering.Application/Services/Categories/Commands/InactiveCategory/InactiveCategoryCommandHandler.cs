using Engineering.Application.Abstractions.Data.Categories;
using Category = Engineering.Domain.Entities.Categories.Category;

namespace Engineering.Application.Services.Categories.Commands.InactiveCategory;

public class InactiveCategoryCommandHandler : ICommandHandler<InactiveCategoryCommand, Category>
{
    private readonly ILogger<InactiveCategoryCommand> _logger;
    private readonly ICategoryRepository _repository;

    public InactiveCategoryCommandHandler(
        ILogger<InactiveCategoryCommand> logger,
        ICategoryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<Category?>> Handle(InactiveCategoryCommand request, CT ct)
    {
        try
        {
            var entity = request.Entity;
            if (entity is null)
                return Result.Failure<Category>(CategoryErrors.CategoryWithIdNotFound);
            if (entity.IsActive == false)
                return Result.Failure<Category>(CategoryErrors.IsInactive);

            entity.SetDeactivate();
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