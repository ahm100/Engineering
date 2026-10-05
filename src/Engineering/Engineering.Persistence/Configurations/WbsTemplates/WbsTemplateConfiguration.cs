using Engineering.Domain.Entities.WbsTemplates;

namespace Engineering.Persistence.Configurations.WbsTemplates;

public class WbsTemplateConfiguration : IEntityTypeConfiguration<WbsTemplate>
{
    private const string TableName = "WbsTemplates";
    public void Configure(EntityTypeBuilder<WbsTemplate> builder)
    {
        builder.MetaConfiguration<WbsTemplate, long>(TableName);

        builder.Property(oo => oo.Code)
            .HasComment(GlobalCmts.Code)
            .HasColumnType("nvarchar(100)")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(oo => oo.Title)
            .HasComment(GlobalCmts.Title)
            .HasColumnType("nvarchar(250)")
            .HasMaxLength(250)
            .IsRequired();

        builder.Property(oo => oo.CompanyId)
            .HasComment(GlobalCmts.CompanyId)
            .IsRequired();

        builder.Property(oo => oo.CompanyId)
            .HasComment(GlobalCmts.CompanyId)
            .IsRequired();

        builder.Property(oo => oo.Description)
            .HasComment(GlobalCmts.Description)
            .HasColumnType("nvarchar(1500)");
    }
}