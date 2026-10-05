using Engineering.Domain.Entities.SessionRecords;

namespace Engineering.Persistence.Configurations.SessionRecords;

public class SessionInviteeConfiguration : IEntityTypeConfiguration<SessionInvitee>
{
    private const string TableName = "SessionInvitees";

    public void Configure(EntityTypeBuilder<SessionInvitee> builder)
    {
        builder.MetaConfiguration<SessionInvitee, long>(TableName);

        builder.Property(oo => oo.Status)
            .HasComment(SessionRecordCmts.Status)
            .IsRequired();

        builder.Property(oo => oo.CompanyId)
            .HasComment(GlobalCmts.CompanyId);

        builder.Property(oo => oo.UserId)
            .HasComment(SessionRecordCmts.UserId);

        builder.HasOne(oo => oo.SessionRecord)
            .WithMany(oo => oo.SessionInvitees)
            .HasForeignKey("SessionRecordId").HasPrincipalKey(nameof(SessionRecord.Id))
            .OnDelete(DeleteBehavior.Cascade);
    }
}