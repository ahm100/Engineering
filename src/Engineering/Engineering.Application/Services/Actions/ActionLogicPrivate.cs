namespace Engineering.Application.Services.Actions;

public partial class ActionLogic : IActionLogic
{
    private async Task<bool> IsDuplicateName(string name, CT ct)
    {
        var nameIsDuplicate = await GetActionByNameHandle(name, ct);
        return nameIsDuplicate is { IsSuccess: true, Value: not null };
    }

    private async Task<bool> IsDuplicateCode(string code, CT ct)
    {
        var codeIsDuplicate = await GetActionByCodeHandle(code, ct);
        return codeIsDuplicate is { IsSuccess: true, Value: not null };
    }
}
