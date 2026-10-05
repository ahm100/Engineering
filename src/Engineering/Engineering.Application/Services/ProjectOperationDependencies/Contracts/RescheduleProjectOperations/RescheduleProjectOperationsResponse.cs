using Engineering.Domain.Entities.ProjectOperations.Enums;

namespace Engineering.Application.Services.ProjectOperationDependencies.Contracts.RescheduleProjectOperations;

public class ProjectOperationScheduleModel
{
    public long Id { get; set; }

    public DateTime? PlannedStartDate { get; set; }

    public DateTime? PlannedFinishDate { get; set; }

    public int? PlannedDuration { get; set; }


    public List<ProjectOperationPredecessorModel> Predecessors { get; set; }
        = new();
}


public class ProjectOperationPredecessorModel
{
    public long Id { get; set; }

    public DateTime? PlannedStartDate { get; set; }

    public DateTime? PlannedFinishDate { get; set; }

    public int? PlannedDuration { get; set; }


    public ProjectOperationDependencyType DependencyType { get; set; }

    public int LagDays { get; set; }
}