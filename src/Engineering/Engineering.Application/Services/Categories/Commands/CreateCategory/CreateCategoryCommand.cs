using Engineering.Application.Services.Categories.Models.CreateCategory;

namespace Engineering.Application.Services.Categories.Commands.CreateCategory;

public record CreateCategoryCommand(
    string CategoryName,
    string CategoryCode,
    bool IsActive,
    long? CompanyId)
    : ICommand<CreateCategoryResponse?>;