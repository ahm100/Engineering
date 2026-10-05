using Category = Engineering.Domain.Entities.Categories.Category;

namespace Engineering.Application.Services.Categories.Commands.StateChangerCategories;

public record StateChangerCategoriesCommand(
    List<Category> Items,
    bool State)
    : ICommand<bool?>;