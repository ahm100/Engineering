using Engineering.Application.Abstractions.Data.Categories;
using Engineering.Application.Services.Categories.Models.UpdateCategory;

namespace Engineering.Application.Services.Categories.Commands.UpdateCategory;

public class UpdateCategoryCommandHandler : ICommandHandler<UpdateCategoryCommand, UpdateCategoryResponse>
{
    private readonly ILogger<UpdateCategoryCommand> _logger;
    private readonly ICategoryRepository _repository;

    public UpdateCategoryCommandHandler(
        ILogger<UpdateCategoryCommand> logger,
        ICategoryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<UpdateCategoryResponse?>> Handle(UpdateCategoryCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.GetCategoryById(request.Id, ct);
            if (entity is null)
                return Result.Failure<UpdateCategoryResponse>(CategoryErrors.CategoryWithIdNotFound);

            entity.SetName(request.CategoryName);
            entity.SetCode(request.CategoryCode);
            entity.SetCompanyId(request.CompanyId);
            if (request.IsActive != entity.IsActive)
            {
                if (request.IsActive)
                    entity.SetActive();
                else
                    entity.SetDeactivate();
            }

            await _repository.Update(entity);
            return new UpdateCategoryResponse(
                entity.Id, entity.CategoryName, entity.CategoryCode, entity.IsActive);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<UpdateCategoryResponse>(SharedErrors.UnknownError);
        }
    }
}