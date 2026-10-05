using CostOver = Engineering.Domain.Entities.CostOvers.CostOver;

namespace Engineering.Application.Services.CostOvers.Commands.StateChangerCostOvers;

public record StateChangerCostOversCommand(
    List<CostOver> Items,
    bool State)
    : ICommand<bool?>;