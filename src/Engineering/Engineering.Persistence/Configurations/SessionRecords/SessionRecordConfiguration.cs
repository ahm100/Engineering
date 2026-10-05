using Engineering.Domain.Entities.SessionRecords;

namespace Engineering.Persistence.Configurations.SessionRecords;

public class SessionRecordConfiguration : IEntityTypeConfiguration<SessionRecord>
{
    private const string TableName = "SessionRecords";

    public void Configure(EntityTypeBuilder<SessionRecord> builder)
    {
        builder.MetaActiveConfiguration<SessionRecord, long>(TableName);

        builder.Property(oo => oo.TitleFa)
            .HasComment(GlobalCmts.TitleFa)
            .HasColumnType("nvarchar(250)")
            .HasMaxLength(250)
            .IsRequired();

        builder.Property(oo => oo.TitleEn)
            .HasComment(GlobalCmts.TitleEn)
            .HasColumnType("nvarchar(250)")
            .HasMaxLength(250);

        builder.Property(oo => oo.ProjectName)
            .HasComment(ProjectCmts.ProjectName)
            .HasColumnType("nvarchar(250)")
            .HasMaxLength(250);

        builder.Property(oo => oo.ContractNumber)
            .HasComment(ContractCmts.ContractNumber);

        builder.Property(oo => oo.SessionDate)
            .HasComment(SessionRecordCmts.SessionDate);

        builder.Property(oo => oo.StartTime)
            .HasComment(SessionRecordCmts.StartTime)
            .IsRequired();

        builder.Property(oo => oo.EndTime)
            .HasComment(SessionRecordCmts.EndTime)
            .IsRequired();

        builder.Property(oo => oo.Location)
            .HasComment(SessionRecordCmts.Location)
            .HasColumnType("nvarchar(500)")
            .HasMaxLength(500);


        builder.Property(oo => oo.SessionCategory)
            .HasComment(SessionRecordCmts.SessionCategory)
            .IsRequired();

        builder.Property(oo => oo.SessionType)
            .HasComment(SessionRecordCmts.SessionType)
            .IsRequired();

        builder.HasOne(oo => oo.Project)
            .WithMany(e => e.SessionRecords)
            .HasForeignKey(oo => oo.ProjctId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder.HasOne(oo => oo.Contract)
            .WithMany(e => e.SessionRecords)
            .HasForeignKey(oo => oo.ContractId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}