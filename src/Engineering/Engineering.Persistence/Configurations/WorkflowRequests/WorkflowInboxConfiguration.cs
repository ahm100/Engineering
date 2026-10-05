using Engineering.Domain.Entities.WorkflowRequests;

namespace Engineering.Persistence.Configurations.WorkflowRequests;

public sealed class WorkflowInboxConfiguration : IEntityTypeConfiguration<WorkflowInbox>
{
    private const string TableName = "WorkflowInbox";

    /// <summary>کلید اصلی خودکار Id، یکتایی رویداد خارجی و ارتباط رسید با درخواست را تنظیم می کند.</summary>
    public void Configure(EntityTypeBuilder<WorkflowInbox> builder)
    {
        builder.MetaConfiguration<WorkflowInbox, long>(TableName);

        builder.HasIndex(x => x.EventId).IsUnique();

        builder.Property(x => x.EventId)
            .HasComment(WorkflowInboxCmts.EventId).ValueGeneratedNever();

        builder.Property(x => x.CompanyId)
            .HasComment(WorkflowInboxCmts.CompanyId);

        builder.Property(x => x.RequestId)
            .HasComment(WorkflowInboxCmts.RequestId);

        builder.Property(x => x.ProcessedAtUtc).
            HasComment(WorkflowInboxCmts.ProcessedAtUtc);

        builder.Property(x => x.PayloadHash)
            .HasComment(WorkflowInboxCmts.PayloadHash)
            .HasMaxLength(64)
            .IsUnicode(false)
            .IsRequired();

        builder.HasOne<WorkflowRequest>()
            .WithMany()
            .HasForeignKey(x => x.RequestId)
            .HasPrincipalKey(x => x.RequestId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
