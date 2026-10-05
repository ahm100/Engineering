using Engineering.Domain.Entities.Categories;
using Branch = Engineering.Domain.Entities.Branchs.Branch;

namespace Engineering.Application.Services.Branchs.Commands.UpdateBranch;

public record UpdateBranchCommand(
    long Id,
    Category Category,
    string BranchName,
    string BranchCode,
    bool IsActive,
    long? CompanyId)
    : ICommand<Branch>;