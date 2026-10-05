using Branch = Engineering.Domain.Entities.Branchs.Branch;

namespace Engineering.Application.Services.Branchs.Commands.ActiveBranch;

public record ActiveBranchCommand(
    Branch Entity)
    : ICommand<Branch>;