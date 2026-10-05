using Engineering.Domain.Entities.SessionRecords;

namespace Engineering.Persistence.Configurations.SessionRecords;

public class SessionRecordActionConfiguration : IEntityTypeConfiguration<SessionRecordAction>
{
    private const string TableName = "SessionRecordActions";

    public void Configure(EntityTypeBuilder<SessionRecordAction> builder)
    {
        builder.MetaActiveConfiguration<SessionRecordAction, long>(TableName);

        builder.Property(oo => oo.Description)
            .HasComment(GlobalCmts.Description)
            .HasColumnType("nvarchar(1500)")
            .HasMaxLength(1500)
            .IsRequired();

        builder.Property(oo => oo.Status)
            .HasComment(SessionRecordCmts.SessionREcordActionStatus)
            .IsRequired();

        builder.Property(oo => oo.Deadline)
            .HasComment(SessionRecordCmts.Deadline)
            .IsRequired();

        builder.Property(oo => oo.UserId)
            .HasComment(SessionRecordCmts.UserId)
            .IsRequired();

        builder.HasOne(oo => oo.SessionRecord)
            .WithMany(oo => oo.SessionRecordActions)
            .HasForeignKey("SessionRecordId").HasPrincipalKey(nameof(SessionRecord.Id))
            .OnDelete(DeleteBehavior.Cascade);
    }
}