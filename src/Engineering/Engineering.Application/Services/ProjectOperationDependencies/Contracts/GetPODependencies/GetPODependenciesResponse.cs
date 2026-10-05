using Engineering.Domain.Entities.ProjectOperations.Enums;

namespace Engineering.Application.Services.ProjectOperationDependencies.Contracts.GetPODependencies;

public record GetPODependenciesResponse(
    List<GetPODependenciesPredecessorModel>? Predecessors,
    List<GetPODependenciesSuccessorModel>? Successors);

public class GetPODependenciesPredecessorModel
{
    public long PredecessorId { get; set; }
    public string PredecessorName { get; set; } = string.Empty;
    public long LagDays { get; set; }
    public DateTime? StartDate { get; set; }
    public string? StartDateShamsi => StartDate.ToShamsi();
    public DateTime? EndDate { get; set; }
    public string? EndDateShamsi => EndDate.ToShamsi();
    public ProjectOperationDependencyType DependencyType { get; set; }
    public string? DependencyTypeDescription => DependencyType.GetEnumDescription();
}

public class GetPODependenciesSuccessorModel
{
    public long SuccessorId { get; set; }
    public string SuccessorName { get; set; } = string.Empty;
    public long LagDays { get; set; }
    public DateTime? StartDate { get; set; }
    public string? StartDateShamsi => StartDate.ToShamsi();
    public DateTime? EndDate { get; set; }
    public string? EndDateShamsi => EndDate.ToShamsi();
    public ProjectOperationDependencyType DependencyType { get; set; }
    public string? DependencyTypeDescription => DependencyType.GetEnumDescription();
}