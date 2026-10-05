using Branch = Engineering.Domain.Entities.Branchs.Branch;

namespace Engineering.Application.Services.Branchs.Commands.StateChangerBranchs;

public record StateChangerBranchsCommand(
    List<Branch> Items,
    bool State)
    : ICommand<bool?>;
