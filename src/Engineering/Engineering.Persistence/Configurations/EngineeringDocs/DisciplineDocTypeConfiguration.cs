
using Engineering.Domain.Entities.EngineeringDocs;


namespace Engineering.Persistence.Configurations.EngineeringDocs;


public class DisciplineDocTypeConfiguration : IEntityTypeConfiguration<DisciplineDocType>
{
    private const string _tableName = "DisciplineDocTypes";

    public void Configure(EntityTypeBuilder<DisciplineDocType> builder)
    {
        builder.ToTable(_tableName);
        builder.HasKey(oo => oo.Id);

        builder.Property(oo => oo.Code)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(oo => oo.Description)
            .HasMaxLength(1500)
            .IsRequired();

        builder.HasOne(oo => oo.Discipline)
            .WithMany(oo => oo.DisciplineDocTypes)
            .HasForeignKey(oo => oo.DisciplineId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(oo => oo.DisciplineDoc)
            .WithMany(oo => oo.DisciplineDocTypes)
            .HasForeignKey(oo => oo.DisciplineDocId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
