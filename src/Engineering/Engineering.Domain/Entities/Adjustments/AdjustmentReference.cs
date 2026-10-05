namespace Engineering.Domain.Entities.Adjustments;

public class AdjustmentReference : ActivateEntity<AdjustmentReference>
{
    [Description(GlobalCmts.Title)]
    public string Title { get; private set; } = string.Empty;

    private AdjustmentReference()
    {
    }

    public AdjustmentReference(
        string title,
        bool isActive)
    {
        SetTitle(title);

        if (isActive)
            SetActive();
        else
            SetDeactivate();
    }

    public void SetTitle(string value)
    {
        Title = Guard.Against.NullOrWhiteSpace(value, nameof(value));
    }
}