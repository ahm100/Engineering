using Engineering.Domain.Entities.EngineeringConfig;

namespace Engineering.Persistence.Configurations.EngineeringConfigs;

public class EngineeringCodingConfigConfiguraton : IEntityTypeConfiguration<EngineeringCodingConfig>
{
    private const string TableName = "EngineeringCodingConfigs";
    public void Configure(EntityTypeBuilder<EngineeringCodingConfig> builder)
    {
        builder.MetaActiveConfiguration<EngineeringCodingConfig, long>(TableName);

        builder.Property(oo => oo.Type)
            .HasComment(EngineeringConfigCmts.CodingAlgorithmType)
            .IsRequired();

        builder.Property(oo => oo.Prefix)
            .HasComment(EngineeringConfigCmts.Prefix)
            .HasMaxLength(25)
            .IsRequired();
    }
}