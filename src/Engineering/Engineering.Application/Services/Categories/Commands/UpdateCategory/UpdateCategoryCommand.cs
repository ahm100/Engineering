using Engineering.Application.Services.Categories.Models.UpdateCategory;

namespace Engineering.Application.Services.Categories.Commands.UpdateCategory;

public record UpdateCategoryCommand(
    long Id,
    string CategoryName,
    string CategoryCode,
    bool IsActive,
    long? CompanyId)
    : ICommand<UpdateCategoryResponse>;