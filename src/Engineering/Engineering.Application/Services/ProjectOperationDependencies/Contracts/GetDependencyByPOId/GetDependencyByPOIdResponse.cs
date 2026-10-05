using Engineering.Domain.Entities.ProjectOperations.Enums;

namespace Engineering.Application.Services.ProjectOperationDependencies.Contracts.GetDependencyByPOId;

public record GetDependencyByPOIdResponse(
    List<GetDependencyByPOIdModel> Data,
    int Count);

public record GetDependencyByPOIdModel
{
    public long Id { get; set; }
    public long PredecessorId { get; set; }
    public string PredecessorName { get; set; } = string.Empty;
    public long SuccessorId { get; set; }
    public string SuccessorName { get; set; } = string.Empty;
    public int LagDays { get; set; }
    public ProjectOperationDependencyType DependencyType { get; set; }
}