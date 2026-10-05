using Engineering.Domain.Entities.Branchs;
using Engineering.Domain.Entities.Seasons;

namespace Engineering.Persistence.Configurations.Seasons;

public class SeasonConfiguration : IEntityTypeConfiguration<Season>
{
    private const string TableName = "EngineeringSeasons";
    public void Configure(EntityTypeBuilder<Season> builder)
    {
        builder.MetaActiveConfiguration<Season, long>(TableName);

        builder.Property(oo => oo.SeasonName)
            .HasColumnType("nvarchar(250)")
            .HasMaxLength(250)
            .IsUnicode(true)
            .IsRequired();

        builder.Property(oo => oo.SeasonCode)
            .HasColumnType("nvarchar(100)")
            .HasMaxLength(100)
            .IsUnicode(true)
            .IsRequired();

        builder.Property(oo => oo.CompanyId);

        builder.Property(oo => oo.PreferentialReferenceCode)
            .HasComment("کد مرجع تفصیلی")
            .IsRequired();

        builder.HasOne(oo => oo.Branch)
            .WithMany(oo => oo.Seasons)
            .HasForeignKey("BranchId").HasPrincipalKey(nameof(Branch.Id))
            .OnDelete(DeleteBehavior.NoAction);

    }
}