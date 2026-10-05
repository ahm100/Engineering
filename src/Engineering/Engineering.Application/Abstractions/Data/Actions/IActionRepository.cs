using Action = Engineering.Domain.Entities.Actions.Action;

namespace Engineering.Application.Abstractions.Data.Actions;

public interface IActionRepository : IBaseRepository<Action>
{
    Task<Action?> GetActionById(
        long id, CT ct);

    Task<List<Action>> GetActions(
        List<long> ids, CT ct);

    Task<List<Action>> GetsActiveActions(
        string? filterData,
        string? code,
        string? name,
        int pageIndex,
        int pageSize, CT ct);

    Task<List<Action>> GetFilteredActions(
        string? filterData,
        string? code,
        string? name,
        int pageIndex,
        int pageSize, CT ct);

    Task<string> CodeCreator(CT ct);

    Task<Action?> GetActionByName(
        string name,
        CT ct);

    Task<Action?> GetActionByCode(
        string code,
        CT ct);
}
