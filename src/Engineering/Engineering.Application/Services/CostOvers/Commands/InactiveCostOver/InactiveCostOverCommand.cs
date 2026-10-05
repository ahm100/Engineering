using CostOver = Engineering.Domain.Entities.CostOvers.CostOver;

namespace Engineering.Application.Services.CostOvers.Commands.InactiveCostOver;

public record InactiveCostOverCommand(
    CostOver Entity)
    : ICommand<CostOver>;