using Engineering.Application.Services.ProjectOperations.Models.SetPlannedDate;

namespace Engineering.Application.Services.ProjectOperations.Command.SetPlannedDate;

public record SetPlannedDateCommand(
    long Id,
    DateTime? PlannedStartDate,
    DateTime? PlannedFinishDate,
    int? PlannedDuration) : ICommand<SetPlannedDateResponse?>;