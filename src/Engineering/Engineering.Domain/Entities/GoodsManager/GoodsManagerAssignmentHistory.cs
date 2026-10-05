namespace Engineering.Domain.Entities.GoodsManager;

[Description(GlobalCmts.Histories)]
public class GoodsManagerAssignmentHistory : AuditableEntity<GoodsManagerAssignmentHistory>
{
    [Description(RGSCmts.Product)]
    public long? ProductId { get; private set; }

    [Description(RGSCmts.OrganizationId)]
    public long OrganizationId { get; private set; }

    [Description(GlobalCmts.Description)]
    public string? Description { get; private set; }

    [Description(RGSCmts.GoodsManagerAssignment)]
    public long GoodsManagerAssignmentId { get; private set; }
    public GoodsManagerAssignment GoodsManagerAssignment { get; private set; }

    public GoodsManagerAssignmentHistory(GoodsManagerAssignment assignment) : this()
    {
        SetGoodsManagerAssignment(assignment);
        SetOrganizationId(assignment.OrganizationId);
        SetProductId(assignment.ProductId);
    }

    public GoodsManagerAssignmentHistory(
        GoodsManagerAssignment assignment,
        long? userId,
        string? description) : this()
    {
        SetGoodsManagerAssignment(assignment);
        SetOrganizationId(assignment.OrganizationId);
        SetProductId(assignment.ProductId);

        CreatorId = userId ?? 0;
        CheckUser = true;
        Created = DateTime.UtcNow;
        Description = description;
    }

    public void SetGoodsManagerAssignment(GoodsManagerAssignment value)
    {
        GoodsManagerAssignment = Guard.Against.Null(value, nameof(value));
        GoodsManagerAssignmentId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    public void SetProductId(long value)
    {
        ProductId = Guard.Against.Null(value, nameof(value));
    }

    public void SetOrganizationId(long value)
    {
        OrganizationId = Guard.Against.Null(value, nameof(value));
    }

#pragma warning disable CS8618
    private GoodsManagerAssignmentHistory() { }
#pragma warning restore CS8618
}