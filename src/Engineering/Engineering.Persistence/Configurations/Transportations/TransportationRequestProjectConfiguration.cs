using Engineering.Domain.Entities.Projects;
using Engineering.Domain.Entities.Transportations;

namespace Engineering.Persistence.Configurations.Transportations;

public class TransportationRequestProjectConfiguration : IEntityTypeConfiguration<TransportationRequestProject>
{
    private const string _tableName = "TransportationRequestProjects";
    public void Configure(EntityTypeBuilder<TransportationRequestProject> builder)
    {
        builder.ToTable(_tableName);
        builder.HasKey(oo => oo.Id);

        builder.Property(oo => oo.Id)
            .UseIdentityColumn(2L, 1)
            .IsRequired();

        builder.Property(oo => oo.IsDeleted)
            .HasDefaultValue(false)
            .IsRequired();

        builder.Property(oo => oo.Created)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(oo => oo.CreatorId)
            .IsRequired();


        builder.HasOne(oo => oo.TransportationRequest)
            .WithMany(oo => oo.TransportationRequestProjects)
            .HasForeignKey("TransportationRequestId").HasPrincipalKey(nameof(TransportationRequest.Id))
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(oo => oo.Project)
            .WithMany(oo => oo.TransportationRequestProjects)
            .HasForeignKey("ProjectId").HasPrincipalKey(nameof(Project.Id))
            .OnDelete(DeleteBehavior.NoAction);

    }
}
