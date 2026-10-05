using CostOver = Engineering.Domain.Entities.CostOvers.CostOver;

namespace Engineering.Application.Services.CostOvers.Commands.DisableCostOver;

public record DisableCostOverCommand(
    CostOver Entity)
    : ICommand<CostOver>;