using Engineering.Domain.Entities.ProjectOperations;
namespace Engineering.Application.Services.ProjectOperations.Command.UpdatePrice;

public record UpdatePriceCommand(
    ProjectOperation ProjectOperation,
    decimal? ChangedPrice,
    int? IncreaseRate
) : ICommand<List<ProjectOperation?>?>;

