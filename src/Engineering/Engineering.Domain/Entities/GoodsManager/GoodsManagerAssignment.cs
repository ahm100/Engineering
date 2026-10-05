namespace Engineering.Domain.Entities.GoodsManager;

[Description(RGSCmts.GoodsManagerAssignment)]
public class GoodsManagerAssignment : AuditableEntity<GoodsManagerAssignment>
{
    [Description(RGSCmts.OrganizationId)]
    public long OrganizationId { get; private set; }

    [Description(RGSCmts.Product)]
    public long ProductId { get; private set; }

    public GoodsManagerAssignment(
        long organizationId,
        long productId) : this()
    {
        SetOrganizationId(organizationId);
        SetProductId(productId);
        AddHistory();
    }

    public static GoodsManagerAssignment Create(
        long organizationId,
        long productId)
    {
        return new GoodsManagerAssignment(
            organizationId,
            productId);
    }

    #region Setters

    public void SetProductId(long value)
    {
        ProductId = Guard.Against.Null(value, nameof(value));
    }

    public void SetOrganizationId(long value)
    {
        OrganizationId = Guard.Against.Null(value, nameof(value));
    }

    public void AddHistory()
    {
        _histories.Add(new GoodsManagerAssignmentHistory(this));
    }

    public void AddHistory(long? userId, string? description)
    {
        _histories.Add(new GoodsManagerAssignmentHistory(this, userId, description));
    }

    public void SetIsDeleted()
    {
        IsDeleted = true;
    }

    public void Update(
        long organizationId,
        long productId)
    {
        SetOrganizationId(organizationId);
        SetProductId(productId);
        AddHistory();
    }

    #endregion

#pragma warning disable CS8618
    private List<GoodsManagerAssignmentHistory> _histories;
    public IReadOnlyList<GoodsManagerAssignmentHistory> Histories => _histories;

    private GoodsManagerAssignment()
    {
        _histories = [];
    }
#pragma warning restore CS8618
}