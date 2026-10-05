using Engineering.Domain.Entities.WorkflowRequests;

namespace Engineering.Persistence.Configurations.WorkflowRequests;

public class WorkflowOutboxConfiguration
    : IEntityTypeConfiguration<WorkflowOutbox>
{
    private const string TableName = "WorkflowOutboxes";

    /// <summary>
    /// ساختار پیام های خروجی، ارتباط با درخواست و ایندکس پردازش را تنظیم می کند.
    /// </summary>
    public void Configure(EntityTypeBuilder<WorkflowOutbox> builder)
    {
        builder.MetaConfiguration<WorkflowOutbox, long>(TableName);

        builder.HasIndex(x => x.MessageId)
            .IsUnique();

        builder.Property(x => x.ProcessedAtUtc)
            .HasComment(WorkflowOutboxCmts.ProcessedAtUtc);

        builder.Property(x => x.LockId)
            .HasComment(WorkflowOutboxCmts.LockId);

        builder.Property(x => x.LockedUntilUtc)
            .HasComment(WorkflowOutboxCmts.LockedUntilUtc);

        builder.Property(x => x.MessageId)
            .HasComment(WorkflowOutboxCmts.MessageId)
            .ValueGeneratedNever();

        builder.Property(x => x.RequestId)
            .HasComment(WorkflowOutboxCmts.RequestId)
            .IsRequired();

        builder.Property(x => x.CompanyId)
            .HasComment(WorkflowOutboxCmts.CompanyId)
            .IsRequired();

        builder.Property(x => x.MessageType)
            .HasComment(WorkflowOutboxCmts.MessageType)
            .HasMaxLength(100)
            .IsUnicode(false)
            .IsRequired();

        builder.Property(x => x.PayloadJson)
            .HasComment(WorkflowOutboxCmts.PayloadJson)
            .HasColumnType("nvarchar(max)")
            .IsRequired();

        builder.Property(x => x.CreatedAtUtc)
            .HasComment(WorkflowOutboxCmts.CreatedAtUtc)
            .IsRequired();

        builder.Property(x => x.NextAttemptAtUtc)
            .HasComment(WorkflowOutboxCmts.NextAttemptAtUtc)
            .IsRequired();

        builder.Property(x => x.Attempts)
            .HasComment(WorkflowOutboxCmts.Attempts)
            .IsRequired();

        builder.Property(x => x.IsSuspended)
            .HasComment(WorkflowOutboxCmts.IsSuspended)
            .IsRequired();

        builder.Property(x => x.LastError)
            .HasComment(WorkflowOutboxCmts.LastError)
            .HasMaxLength(2000);

        builder.Property(x => x.RowVersion)
            .IsRowVersion();

        builder.HasOne<WorkflowRequest>()
            .WithMany()
            .HasForeignKey(x => x.RequestId)
            .HasPrincipalKey(x => x.RequestId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => new
        {
            x.RequestId,
            x.MessageType
        }).IsUnique().HasFilter("[MessageType] = 'workflow.start.v1'");

        builder.HasIndex(x => new
        {
            x.NextAttemptAtUtc,
            x.CreatedAtUtc
        }).HasFilter("[ProcessedAtUtc] IS NULL AND [IsSuspended] = 0");
    }
}
