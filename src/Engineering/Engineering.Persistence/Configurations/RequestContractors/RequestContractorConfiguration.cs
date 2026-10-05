using Engineering.Domain.Entities.ProjectOperationDetails;
using Engineering.Domain.Entities.RequestContractors;
using Engineering.Domain.Entities.RequestContractors.Enums;
using Engineering.Domain.Entities.ServiceInfos;

namespace Engineering.Persistence.Configurations.RequestContractors;

public class RequestContractorConfiguration : IEntityTypeConfiguration<RequestContractor>
{
    private const string _tableName = "RequestContractors";
    public void Configure(EntityTypeBuilder<RequestContractor> builder)
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
        .ValueGeneratedOnAdd().
        IsRequired();

        builder.Property(oo => oo.CreatorId)
            .IsRequired();

        builder.Property(oo => oo.RequestNumber)
       .IsRequired(false)
       .HasDefaultValueSql("NEXT VALUE FOR engineer.RequestContractor_RequestNumber");

        builder.Property(oo => oo.CompanyId);

        builder.Property(oo => oo.Status)
            .HasDefaultValue(RequestContractorStatus.New)
            .IsRequired();

        builder.Property(oo => oo.Description)
            .HasColumnType("nvarchar(1500)");

        builder.Property(oo => oo.StatusDescription)
            .HasColumnType("nvarchar(1500)");

        builder.Property(oo => oo.Volume)
               .HasColumnType("decimal(18,2)")
               .HasDefaultValue(0)
               .IsRequired();

        builder.HasOne(oo => oo.ProjectOperationDetail)
               .WithMany(oo => oo.RequestContractors)
               .HasForeignKey("ProjectOperationDetailId")
               .HasPrincipalKey(nameof(ProjectOperationDetail.Id))
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(oo => oo.ServiceInfo)
               .WithMany(oo => oo.RequestContractors)
               .HasForeignKey("ServiceInfoId")
               .HasPrincipalKey(nameof(ServiceInfo.Id))
               .OnDelete(DeleteBehavior.Restrict);
    }
}
