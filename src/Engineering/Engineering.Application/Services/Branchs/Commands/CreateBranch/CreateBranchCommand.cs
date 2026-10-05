using Branch = Engineering.Domain.Entities.Branchs.Branch;
using Category = Engineering.Domain.Entities.Categories.Category;

namespace Engineering.Application.Services.Branchs.Commands.CreateBranch;

public record CreateBranchCommand(
    Category Category,
    string BranchName,
    string BranchCode,
    bool IsActive,
    long? CompanyId)
    : ICommand<Branch?>;