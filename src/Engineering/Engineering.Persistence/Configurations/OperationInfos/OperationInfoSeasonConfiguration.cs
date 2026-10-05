
using Engineering.Domain.Entities.OperationInfos;

namespace Engineering.Persistence.Configurations.OperationInfoSeasons;

public class OperationInfoSeasonConfiguration : IEntityTypeConfiguration<OperationInfoSeason>
{
    private const string _tableName = "OperationInfoSeasons";
    public void Configure(EntityTypeBuilder<OperationInfoSeason> builder)
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


        builder.HasOne(oo => oo.Season)
            .WithMany(oo => oo.OperationInfoSeasons)
            .HasForeignKey("SeasonId").HasPrincipalKey(a => a.Id)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(oo => oo.OperationInfo)
            .WithMany(oo => oo.OperationInfoSeasons)
            .HasForeignKey("OperationInfoId").HasPrincipalKey(a => a.Id)
            .OnDelete(DeleteBehavior.NoAction);
    }
}