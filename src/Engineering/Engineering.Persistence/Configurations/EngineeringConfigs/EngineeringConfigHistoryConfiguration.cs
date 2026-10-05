using Engineering.Domain.Entities.EngineeringConfig;

namespace Engineering.Persistence.Configurations.EngineeringConfigs;

public class EngineeringConfigHistoryConfiguration : IEntityTypeConfiguration<EngineeringConfigHistory>
{
    private const string TableName = "EngineeringConfigHistories";
    public void Configure(EntityTypeBuilder<EngineeringConfigHistory> builder)
    {
        builder.MetaActiveConfiguration<EngineeringConfigHistory, long>(TableName);

        builder.Property(oo => oo.SendTelegramMessage)
            .HasComment(EngineeringConfigCmts.SendTelegramMessage)
            .IsRequired();

        builder.Property(oo => oo.ProjectThirdParties)
            .HasComment(EngineeringConfigCmts.ProjectThirdParties)
            .HasDefaultValue(false)
            .IsRequired();

        builder.HasOne(oo => oo.EngineeringConfig)
            .WithMany(oo => oo.EngineeringConfigHistories)
            .HasForeignKey(oo => oo.EngineeringConfigId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();
    }
}