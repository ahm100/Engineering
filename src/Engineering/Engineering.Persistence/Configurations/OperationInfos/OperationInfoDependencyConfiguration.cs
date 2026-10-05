using Engineering.Domain.Entities.OperationInfos;

namespace Engineering.Persistence.Configurations.OperationInfos;

public class OperationInfoDependencyConfiguration : IEntityTypeConfiguration<OperationInfoDependency>
{
    private const string _tableName = "OperationInfoDependencies";
    public void Configure(EntityTypeBuilder<OperationInfoDependency> builder)
    {
        builder.ToTable(_tableName);
        builder.HasKey(oo => oo.Id);

        builder.Property(oo => oo.Id)
            .UseIdentityColumn(2L, 1)
            .IsRequired();

        builder.Property(oo => oo.RelationId)
            .IsRequired();

        builder.Property(oo => oo.WorkingDays)
            .IsRequired();

        builder.Property(oo => oo.DependencyType)
            .IsRequired();

        builder.Property(oo => oo.IsDeleted)
            .HasDefaultValue(false)
            .IsRequired();

        builder.Property(oo => oo.Created)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(oo => oo.CreatorId)
            .IsRequired();


        builder.HasOne(a => a.OperationInfo)
            .WithMany(a => a.OperationInfoDependencies)
            .HasForeignKey("OperationInfoId").HasPrincipalKey(o => o.Id)
            .OnDelete(DeleteBehavior.NoAction);

    }
}