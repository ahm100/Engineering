using Engineering.Domain.Entities.Projects;
using Engineering.Domain.Entities.Projects.Histories;

namespace Engineering.Persistence.Configurations.Projects;

public class SubProjectHistoryConfiguration : IEntityTypeConfiguration<SubProjectHistory>
{
    private const string _tableName = "SubProjectHistories";

    public void Configure(EntityTypeBuilder<SubProjectHistory> builder)
    {
        builder.MetaConfiguration<SubProjectHistory, long>(_tableName);

        builder.Property(oo => oo.Name)
            .HasColumnType("nvarchar(250)")
            .HasMaxLength(250)
            .IsRequired();

        builder.Property(oo => oo.Description)
            .HasColumnType("nvarchar(1500)")
            .HasMaxLength(1500);

        builder.Property(oo => oo.Operation)
            .IsRequired();

        builder.Property(oo => oo.Type)
            .IsRequired();

        builder.Property(oo => oo.Status)
            .IsRequired();

        builder.Property(oo => oo.StartDate)
            .IsRequired();

        builder.HasIndex(oo => oo.SubProjectId);

        builder.HasOne(oo => oo.SubProject)
            .WithMany()
            .HasForeignKey(oo => oo.SubProjectId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
