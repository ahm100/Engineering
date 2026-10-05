using Branch = Engineering.Domain.Entities.Branchs.Branch;

namespace Engineering.Application.Services.Branchs.Commands.DisableBranch;

public record DisableBranchCommand(
    Branch Entity)
    : ICommand<Branch>;