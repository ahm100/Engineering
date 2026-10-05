using Engineering.Domain.Entities.OperationLocations;
using Engineering.Domain.Entities.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperations;

namespace Engineering.Persistence.Configurations.ProjectOperationDetails;

public class ProjectOperationDetailConfiguration : IEntityTypeConfiguration<ProjectOperationDetail>
{
    private const string _tableName = "ProjectOperationDetails";
    public void Configure(EntityTypeBuilder<ProjectOperationDetail> builder)
    {
        builder.ToTable(_tableName);
        builder.HasKey(oo => oo.Id);

        builder.Property(oo => oo.Id)
            .UseIdentityColumn(2L, 1)
            .IsRequired();

        builder.Property(oo => oo.Code)
            .HasColumnType("nvarchar(250)");

        builder.Property(oo => oo.StartDate);

        builder.Property(oo => oo.EndDate);

        builder.Property(oo => oo.Length)
            .HasColumnType("decimal(18,5)")
            .HasDefaultValue(1)
            .IsRequired();

        builder.Property(oo => oo.LengthChangeable)
            .HasDefaultValue(true)
            .IsRequired();

        builder.Property(oo => oo.Width)
            .HasColumnType("decimal(18,5)")
            .HasDefaultValue(1)
            .IsRequired();

        builder.Property(oo => oo.WidthChangeable)
            .HasDefaultValue(true)
            .IsRequired();

        builder.Property(oo => oo.Height)
            .HasColumnType("decimal(18,5)")
            .HasDefaultValue(1)
            .IsRequired();

        builder.Property(oo => oo.HeightChangeable)
            .HasDefaultValue(true)
            .IsRequired();

        builder.Property(oo => oo.Weight)
            .HasColumnType("decimal(18,5)")
            .HasDefaultValue(1)
            .IsRequired();

        builder.Property(oo => oo.WeightChangeable)
            .HasDefaultValue(true)
            .IsRequired();

        builder.Property(oo => oo.Number)
            .HasColumnType("decimal(18,5)")
            .HasDefaultValue(1)
            .IsRequired();

        builder.Property(oo => oo.FinalAmount)
            .HasColumnType("decimal(18,5)")
            .HasDefaultValue(1)
            .IsRequired();

        builder.Property(oo => oo.NumberChangeable)
            .HasDefaultValue(true)
            .IsRequired();

        builder.Property(oo => oo.CompanyId);

        builder.Property(oo => oo.Status)
            .IsRequired();

        builder.Property(oo => oo.Priority)
            .IsRequired();

        builder.Property(oo => oo.Day)
            .IsRequired();

        builder.Property(oo => oo.Hour)
            .IsRequired();

        builder.Property(oo => oo.Description)
            .HasColumnType("nvarchar(1500)");

        builder.Property(oo => oo.Priority)
            .IsRequired();

        builder.Property(oo => oo.Created)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(oo => oo.CreatorId)
            .IsRequired();


        builder.HasOne(oo => oo.ProjectOperation)
            .WithMany(oo => oo.ProjectOperationDetails)
            .HasForeignKey("ProjectOperationId").HasPrincipalKey(nameof(ProjectOperation.Id))
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(oo => oo.OperationLocation)
            .WithMany(oo => oo.ProjectOperationDetails)
            .HasForeignKey("OperationLocationId").HasPrincipalKey(nameof(OperationLocation.Id))
            .OnDelete(DeleteBehavior.NoAction);

    }
}