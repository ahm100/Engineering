using Engineering.Domain.Entities.WorkflowRequests;

namespace Engineering.Persistence.Configurations.WorkflowRequests;

public class WorkflowRequestConfiguration
    : IEntityTypeConfiguration<WorkflowRequest>
{
    private const string TableName = "WorkflowRequests";
    /// <summary>
    /// ساختار درخواست های مشترک گردش کار و محدودیت درخواست جاری را تنظیم می کند.
    /// </summary>
    public void Configure(EntityTypeBuilder<WorkflowRequest> builder)
    {
        builder.MetaConfiguration<WorkflowRequest, long>(TableName);

        builder.Property(x => x.RowVersion)
            .IsRowVersion();

        builder.Property(x => x.WorkflowInstanceId)
            .HasComment(WorkflowRequestCmts.WorkflowInstanceId);

        builder.Property(x => x.AcceptedAtUtc)
            .HasComment(WorkflowRequestCmts.AcceptedAtUtc);

        builder.Property(x => x.FinishedAtUtc)
            .HasComment(WorkflowRequestCmts.FinishedAtUtc);

        builder.Property(x => x.RequestId)
            .HasComment(WorkflowRequestCmts.RequestId)
            .IsRequired();

        builder.HasAlternateKey(x => x.RequestId);
        builder.HasIndex(x => new { x.CompanyId, x.WorkflowInstanceId })
            .IsUnique().HasFilter("[WorkflowInstanceId] IS NOT NULL");

        builder.Property(x => x.CompanyId)
            .HasComment(WorkflowRequestCmts.CompanyId)
            .IsRequired();

        builder.Property(x => x.EntityType)
            .HasComment(WorkflowRequestCmts.EntityType)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.BusinessKey)
            .HasComment(WorkflowRequestCmts.BusinessKey)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.Purpose)
            .HasComment(WorkflowRequestCmts.Purpose)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.WorkflowCode)
            .HasComment(WorkflowRequestCmts.WorkflowCode)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.RequestedByUserId)
            .HasComment(WorkflowRequestCmts.RequestedByUserId)
            .IsRequired();

        builder.Property(x => x.VariablesJson)
            .HasComment(WorkflowRequestCmts.VariablesJson)
            .HasColumnType("nvarchar(max)")
            .IsRequired();

        builder.Property(x => x.DispatchStatus)
            .HasComment(WorkflowRequestCmts.DispatchStatus)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(x => x.ExecutionStatus)
            .HasComment(WorkflowRequestCmts.ExecutionStatus)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(x => x.Outcome)
            .HasComment(WorkflowRequestCmts.Outcome)
            .HasMaxLength(100);

        builder.Property(x => x.LastError)
            .HasComment(WorkflowRequestCmts.LastError)
            .HasMaxLength(2000);

        builder.Property(x => x.RequestedAtUtc)
            .HasComment(WorkflowRequestCmts.RequestedAtUtc)
            .IsRequired();

        builder.Property(x => x.IsOpen)
            .HasComment(WorkflowRequestCmts.IsOpen)
            .IsRequired();

        builder.HasIndex(x => new
        {
            x.CompanyId,
            x.EntityType,
            x.BusinessKey,
            x.Purpose
        }).IsUnique().HasFilter("[IsOpen] = 1");

    }
}
