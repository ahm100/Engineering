using Engineering.Domain.Entities.CostCenters;
using Engineering.Domain.Entities.Projects;
using Engineering.Domain.Entities.TelegramChats;

namespace Engineering.Persistence.Configurations.TelegramChats;

public class CostCenterTelegramChatsConfiguration : IEntityTypeConfiguration<TelegramChat>
{
    private const string _tableName = "TelegramChats";
    public void Configure(EntityTypeBuilder<TelegramChat> builder)
    {
        builder.ToTable(_tableName);
        builder.HasKey(oo => oo.Id);

        builder.Property(oo => oo.Id)
            .UseIdentityColumn(2L, 1)
            .IsRequired();

        builder.Property(oo => oo.ChatName)
            .HasColumnType("nvarchar(250)")
            .HasMaxLength(250)
            .IsUnicode(true);

        builder.Property(oo => oo.ChatUrl)
            .HasColumnType("nvarchar(250)")
            .HasMaxLength(250)
            .IsUnicode(true)
            .IsRequired();

        builder.Property(oo => oo.ChatId)
            .HasColumnType("nvarchar(250)")
            .HasMaxLength(100)
            .IsUnicode(true)
            .IsRequired();

        builder.Property(oo => oo.Description)
            .HasColumnType("nvarchar(500)")
            .HasMaxLength(500)
            .IsUnicode(true);

        builder.Property(oo => oo.IsActive)
            .HasDefaultValue(true)
            .IsRequired();

        builder.Property(oo => oo.IsDeleted)
            .HasDefaultValue(false)
            .IsRequired();

        builder.Property(oo => oo.Created)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(oo => oo.CreatorId)
            .IsRequired();


        builder.HasOne(oo => oo.CostCenter)
            .WithMany(oo => oo.TelegramChats)
            .HasForeignKey("CostCenterId").HasPrincipalKey(nameof(CostCenter.Id))
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(oo => oo.Project)
            .WithMany(oo => oo.TelegramChats)
            .HasForeignKey("ProjectId").HasPrincipalKey(nameof(Project.Id))
            .OnDelete(DeleteBehavior.NoAction);

    }
}