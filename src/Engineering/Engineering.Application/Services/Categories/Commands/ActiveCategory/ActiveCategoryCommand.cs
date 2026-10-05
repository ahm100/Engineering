using Category = Engineering.Domain.Entities.Categories.Category;

namespace Engineering.Application.Services.Categories.Commands.ActiveCategory;

public record ActiveCategoryCommand(
    Category Entity)
    : ICommand<Category>;