using Branch = Engineering.Domain.Entities.Branchs.Branch;

namespace Engineering.Application.Services.Branchs.Commands.InactiveBranch;

public record InactiveBranchCommand(
    Branch Entity)
    : ICommand<Branch>;