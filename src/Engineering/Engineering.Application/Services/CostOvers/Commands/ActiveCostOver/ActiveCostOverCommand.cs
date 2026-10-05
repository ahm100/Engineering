using CostOver = Engineering.Domain.Entities.CostOvers.CostOver;

namespace Engineering.Application.Services.CostOvers.Commands.ActiveCostOver;

public record ActiveCostOverCommand(
    CostOver Entity)
    : ICommand<CostOver>;