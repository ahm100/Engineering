using Engineering.Application.Abstractions.Data.Categories;
using Category = Engineering.Domain.Entities.Categories.Category;

namespace Engineering.Application.Services.Categories.Commands.DisableCategory;

public class DisableCategoryCommandHandler : ICommandHandler<DisableCategoryCommand, Category>
{
    private readonly ILogger<DisableCategoryCommand> _logger;
    private readonly ICategoryRepository _repository;

    public DisableCategoryCommandHandler(
        ILogger<DisableCategoryCommand> logger,
        ICategoryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<Category?>> Handle(DisableCategoryCommand request, CT ct)
    {
        try
        {
            var entity = request.Entity;
            if (entity is null)
                return Result.Failure<Category>(CategoryErrors.CategoryWithIdNotFound);
            if (entity.Branchs.Any(a => a.Seasons.Any(a => a.OperationInfoSeasons.Count > 0)))
                return Result.Failure<Category>(CategoryErrors.CanNottDeletebecauseOfOpInfo);
            if (entity.ProjectCategories is not null && entity.ProjectCategories.Any(x => x.Project.IsActive))
                return Result.Failure<Category>(CategoryErrors.CanNottDeletebecauseOfProjects);

            entity.SoftDelete();
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