namespace Engineering.Application.Services.ProjectOperationDependencies.Queries.WouldCreateCycle;

public record WouldCreateCycleQuery(
    long StartId,
    long TargetId) : IQuery<bool?>;