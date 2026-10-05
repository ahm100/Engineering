using Engineering.Domain.Entities.OperationInfos;

namespace Engineering.Persistence.Configurations.OperationInfoGroupRelations;

public class OperationInfoGroupRelationConfiguration : IEntityTypeConfiguration<OperationInfoGroupRelation>
{
    private const string _tableName = "OperationInfoGroupRelations";
    public void Configure(EntityTypeBuilder<OperationInfoGroupRelation> builder)
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


        builder.HasOne(oo => oo.OperationInfoGroup)
            .WithMany(oo => oo.OperationInfoGroupRelations)
            .HasForeignKey("OperationInfoGroupId").HasPrincipalKey(a => a.Id)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(oo => oo.OperationInfo)
            .WithMany(oo => oo.OperationInfoGroupRelations)
            .HasForeignKey("OperationInfoId").HasPrincipalKey(a => a.Id)
            .OnDelete(DeleteBehavior.NoAction);
    }
}