using Engineering.Domain.Entities.Projects;

namespace Engineering.Persistence.Configurations.Projects;

public class SubProjectSequenceConfiguration : IEntityTypeConfiguration<SubProjectSequence>
{
    private const string _tableName = "SubProjectSequences";

    public void Configure(EntityTypeBuilder<SubProjectSequence> builder)
    {
        builder.ToTable(_tableName);
        builder.HasKey(oo => oo.ProjectId);

        builder.Property(oo => oo.ProjectId)
            .ValueGeneratedNever();

        builder.Property(oo => oo.LastSequenceNumber)
            .IsRequired();

        builder.HasOne(oo => oo.Project)
            .WithOne()
            .HasForeignKey<SubProjectSequence>(oo => oo.ProjectId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
