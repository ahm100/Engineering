using Category = Engineering.Domain.Entities.Categories.Category;

namespace Engineering.Application.Services.Categories.Commands.InactiveCategory;

public record InactiveCategoryCommand(
    Category Entity)
    : ICommand<Category>;