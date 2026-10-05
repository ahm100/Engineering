
using Engineering.Domain.Entities.EngineeringDocs;


namespace Engineering.Persistence.Configurations.EngineeringDocs;

public class DisciplineConfiguration : IEntityTypeConfiguration<Discipline>
{
    private const string _tableName = "Disciplines";

    public void Configure(EntityTypeBuilder<Discipline> builder)
    {
        builder.ToTable(_tableName);
        builder.HasKey(oo => oo.Id);

        builder.Property(oo => oo.Code)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(oo => oo.Name)
            .HasMaxLength(1500)
            .IsRequired();

        builder.Property(oo => oo.EnglishName)
            .HasMaxLength(1500)
            .IsRequired();

        builder.Property(oo => oo.Description)
            .HasMaxLength(1500)
            .IsRequired();

        builder.HasMany(oo => oo.DisciplineDocTypes)
            .WithOne(oo => oo.Discipline)
            .HasForeignKey(oo => oo.DisciplineId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(oo => oo.ProjectDocs)
            .WithOne(oo => oo.Discipline)
            .HasForeignKey(oo => oo.DisciplineId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
