using Engineering.Domain.Entities.RequestRewards;

namespace Engineering.Persistence.Configurations.RequestRewards;

public class RequestRewardDocumentConfiguration : IEntityTypeConfiguration<RequestRewardDocument>
{
    private const string _tableName = "RequestRewardDocuments";
    public void Configure(EntityTypeBuilder<RequestRewardDocument> builder)
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

        builder.Property(oo => oo.Url)
       .HasColumnType("nvarchar(1500)");


        builder.HasOne(oo => oo.RequestReward)
            .WithMany(oo => oo.RequestRewardDocuments)
            .HasForeignKey("RequestRewardId").HasPrincipalKey(nameof(RequestReward.Id))
            .OnDelete(DeleteBehavior.Restrict);
    }
}
