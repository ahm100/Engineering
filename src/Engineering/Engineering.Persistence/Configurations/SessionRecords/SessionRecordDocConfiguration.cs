using Engineering.Domain.Entities.SessionRecords;

namespace Engineering.Persistence.Configurations.SessionRecords;

public class SessionRecordDocConfiguration : IEntityTypeConfiguration<SessionRecordDoc>
{
    private const string TableName = "SessionRecordDocs";

    public void Configure(EntityTypeBuilder<SessionRecordDoc> builder)
    {
        builder.MetaConfiguration<SessionRecordDoc, long>(TableName);

        builder.Property(oo => oo.URL)
            .HasComment(GlobalCmts.URL)
            .HasColumnType("nvarchar(1000)")
            .HasMaxLength(1000)
            .IsRequired();

        builder.Property(oo => oo.Type)
            .HasComment(SessionRecordCmts.SessionRecordDocType)
            .IsRequired();

        builder.HasOne(oo => oo.SessionRecord)
            .WithMany(oo => oo.SessionRecordDocs)
            .HasForeignKey("SessionRecordId").HasPrincipalKey(nameof(SessionRecord.Id))
            .OnDelete(DeleteBehavior.Cascade);
    }
}