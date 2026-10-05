using Category = Engineering.Domain.Entities.Categories.Category;

namespace Engineering.Application.Services.Categories.Commands.DisableCategory;

public record DisableCategoryCommand(
    Category Entity)
    : ICommand<Category>;