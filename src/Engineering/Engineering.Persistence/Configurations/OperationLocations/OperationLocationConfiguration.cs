using Engineering.Domain.Entities.CostCenters;
using Engineering.Domain.Entities.OperationLocations;
using Engineering.Domain.Entities.Projects;

namespace Engineering.Persistence.Configurations.OperationLocations;

public class OperationLocationConfiguration : IEntityTypeConfiguration<OperationLocation>
{
    private const string _tableName = "OperationLocations";
    public void Configure(EntityTypeBuilder<OperationLocation> builder)
    {
        builder.ToTable(_tableName);
        builder.HasKey(oo => oo.Id);

        builder.Property(oo => oo.Id)
            .UseIdentityColumn(2L, 1)
            .IsRequired();

        builder.Property(oo => oo.Coding)
            .HasColumnType("nvarchar(max)")
            .IsUnicode(true)
            .IsRequired();

        builder.Property(oo => oo.PublicName)
            .IsRequired();

        builder.Property(oo => oo.PrivateName)
            .IsRequired();

        builder.Property(oo => oo.CompanyId);

        builder.Property(oo => oo.Priority)
            .IsRequired();

        builder.Property(oo => oo.PrivateCode)
            .HasColumnType("nvarchar(250)")
            .HasMaxLength(250)
            .IsUnicode(true)
            .IsRequired();

        builder.Property(oo => oo.PublicCode)
            .IsUnicode(true)
            .IsRequired();

        builder.Property(oo => oo.Path)
            .IsUnicode(true)
            .IsRequired();

        builder.Property(oo => oo.Priority)
            .IsRequired();

        builder.Property(oo => oo.IsActive)
            .IsRequired();

        builder.Property(oo => oo.IsDeleted)
            .HasDefaultValue(false)
            .IsRequired();

        builder.Property(oo => oo.Created)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(oo => oo.CreatorId)
            .IsRequired();


        builder.HasOne(oo => oo.Parent)
            .WithMany(oo => oo.Children)
            .HasForeignKey("ParentId")
            .HasPrincipalKey(nameof(OperationLocation.Id))
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(oo => oo.CostCenter)
            .WithMany(oo => oo.OperationLocations)
            .HasForeignKey("CostCenterId")
            .HasPrincipalKey(nameof(CostCenter.Id))
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(oo => oo.Project)
            .WithMany(oo => oo.OperationLocations)
            .HasForeignKey("ProjectId")
            .HasPrincipalKey(nameof(Project.Id))
            .OnDelete(DeleteBehavior.NoAction);
    }
}