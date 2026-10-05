using Engineering.Domain.Entities.SessionRecords;

namespace Engineering.Persistence.Configurations.SessionRecords;

public class SessionItemConfiguration : IEntityTypeConfiguration<SessionItem>
{
    private const string TableName = "SessionItems";

    public void Configure(EntityTypeBuilder<SessionItem> builder)
    {
        builder.MetaConfiguration<SessionItem, long>(TableName);

        builder.Property(oo => oo.Descriotion)
            .HasComment(GlobalCmts.Description)
            .HasColumnType("nvarchar(1500)")
            .HasMaxLength(1500)
            .IsRequired();

        builder.HasOne(oo => oo.SessionRecord)
            .WithMany(oo => oo.SessionItems)
            .HasForeignKey("SessionRecordId").HasPrincipalKey(nameof(SessionRecord.Id))
            .OnDelete(DeleteBehavior.Cascade);
    }
}