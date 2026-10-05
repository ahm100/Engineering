using Engineering.Domain.Entities.EngineeringDocs;

namespace Engineering.Persistence.Configurations.EngineeringDocs;

public class DisciplineDocConfiguration : IEntityTypeConfiguration<DisciplineDoc>
{
    private const string _tableName = "DisciplineDocs";

    public void Configure(EntityTypeBuilder<DisciplineDoc> builder)
    {
        builder.ToTable(_tableName);
        builder.HasKey(oo => oo.Id);

        builder.Property(oo => oo.Code)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(oo => oo.Title)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(oo => oo.Description)
            .HasMaxLength(1500)
            .IsRequired();

        builder.HasMany(oo => oo.DisciplineDocTypes)
            .WithOne(oo => oo.DisciplineDoc)
            .HasForeignKey(oo => oo.DisciplineDocId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(oo => oo.ProjectDocs)
            .WithOne(oo => oo.DisciplineDoc)
            .HasForeignKey(oo => oo.DisciplineDocId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
