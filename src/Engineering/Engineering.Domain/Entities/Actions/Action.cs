namespace Engineering.Domain.Entities.Actions;

public class Action : ActivateEntity<Action, long>
{
    [Description(GlobalCmts.ActionName)]
    public string ActionName { get; private set; } = string.Empty;
    [Description(GlobalCmts.ActionCode)]
    public string ActionCode { get; private set; } = string.Empty;
    [Description(GlobalCmts.CompanyId)]
    public long? CompanyId { get; private set; }

    public Action(string name, string code, long? companyId) : this()
    {
        SetName(name);
        SetCode(code);
        SetCompanyId(companyId);
        SetActive();
    }

    public void SetName(string value)
    {
        ActionName = Guard.Against.NullOrWhiteSpace(value, nameof(value));
    }

    public void SetCode(string value)
    {
        ActionCode = Guard.Against.NullOrWhiteSpace(value, nameof(value));
    }

    public void SetCompanyId(long? value)
    {
        CompanyId = value;
    }

    public void Update(string name, string code)
    {
        SetName(name);
        SetCode(code);
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private List<OperationInfoAction> _operationInfoActions;
    public IReadOnlyList<OperationInfoAction> OperationInfoActions => _operationInfoActions;
    private Action()
    {
        _operationInfoActions = [];
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
