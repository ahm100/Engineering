using Engineering.Application.Abstractions.Data.Categories;

namespace Engineering.Application.Services.Categories.Commands.StateChangerCategories;

public class StateChangerCategoriesCommandHandler : ICommandHandler<StateChangerCategoriesCommand, bool?>
{
    private readonly ILogger<StateChangerCategoriesCommand> _logger;
    private readonly ICategoryRepository _repository;

    public StateChangerCategoriesCommandHandler(
        ILogger<StateChangerCategoriesCommand> logger,
        ICategoryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<bool?>> Handle(StateChangerCategoriesCommand request, CT ct)
    {
        try
        {
            if (request.State)
                foreach (var item in request.Items)
                {
                    if (item.IsActive != request.State)
                    {
                        item.SetActive();
                        await _repository.Update(item);
                    }
                }
            else
                foreach (var item in request.Items)
                {
                    if (item.IsActive != request.State)
                    {
                        item.SetDeactivate();
                        await _repository.Update(item);
                    }
                }

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<bool?>(SharedErrors.UnknownError);
        }
    }
}