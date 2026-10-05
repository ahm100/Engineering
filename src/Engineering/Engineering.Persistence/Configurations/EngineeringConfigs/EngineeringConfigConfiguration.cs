using Engineering.Domain.Entities.EngineeringConfig;

namespace Engineering.Persistence.Configurations.EngineeringConfigs;

public class EngineeringConfigConfiguration : IEntityTypeConfiguration<EngineeringConfig>
{
    private const string TableName = "EngineeringConfigs";
    public void Configure(EntityTypeBuilder<EngineeringConfig> builder)
    {
        builder.MetaActiveConfiguration<EngineeringConfig, long>(TableName);

        builder.Property(oo => oo.CompanyId)
            .HasComment(GlobalCmts.CompanyId)
            .IsRequired();

        builder.Property(oo => oo.SendTelegramMessage)
            .HasComment(EngineeringConfigCmts.SendTelegramMessage)
            .IsRequired();

        builder.Property(oo => oo.ProjectThirdParties)
            .HasComment(EngineeringConfigCmts.ProjectThirdParties)
            .HasDefaultValue(false)
            .IsRequired();

        builder.Property(oo => oo.HaveCodingAlgorithm)
            .HasComment(EngineeringConfigCmts.HaveCodingAlgorithm)
            .HasDefaultValue(false)
            .IsRequired();
    }
}
