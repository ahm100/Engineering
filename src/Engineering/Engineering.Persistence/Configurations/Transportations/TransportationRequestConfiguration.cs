using Engineering.Domain.Entities.BillOfLadings;
using Engineering.Domain.Entities.CostCenters;
using Engineering.Domain.Entities.MachineTypes;
using Engineering.Domain.Entities.Projects;
using Engineering.Domain.Entities.Seasons;
using Engineering.Domain.Entities.Transportations;
using Engineering.Domain.Entities.Transportations.Enums;
using Engineering.Domain.Entities.Trips;

namespace Engineering.Persistence.Configurations.Transportations;

public class TransportationRequestConfiguration : IEntityTypeConfiguration<TransportationRequest>
{
    private const string _tableName = "TransportationRequests";
    public void Configure(EntityTypeBuilder<TransportationRequest> builder)
    {
        builder.ToTable(_tableName);
        builder.HasKey(oo => oo.Id);

        builder.Property(oo => oo.Id)
            .UseIdentityColumn(2L, 1)
            .IsRequired();

        builder.Property(oo => oo.Passenger)
            .HasColumnType("nvarchar(250)");

        builder.Property(oo => oo.RequestNumber);

        builder.Property(oo => oo.PaymentOrderId);

        builder.Property(oo => oo.PaymentDate);

        builder.Property(oo => oo.StartingCityId);

        builder.Property(oo => oo.DestinationCityId);

        builder.Property(oo => oo.ImageLink);

        builder.Property(oo => oo.StartDate)
            .IsRequired(false);

        builder.Property(oo => oo.EndDate)
            .IsRequired(false);

        builder.Property(oo => oo.Description)
            .HasColumnType("nvarchar(1500)");

        builder.Property(oo => oo.DriverId);

        builder.Property(oo => oo.CompanyId);

        builder.Property(oo => oo.PostageDate);

        builder.Property(oo => oo.ReceivedDate);

        builder.Property(oo => oo.BillOfLadingImage);

        builder.Property(oo => oo.DelivererName)
            .HasColumnType("nvarchar(250)");

        builder.Property(oo => oo.RecipientName)
            .HasColumnType("nvarchar(250)");

        builder.Property(oo => oo.FreightNumber)
            .HasColumnType("nvarchar(250)");

        builder.Property(oo => oo.LoadWeight)
            .HasColumnType("decimal");

        builder.Property(oo => oo.Volume)
            .HasColumnType("decimal");

        builder.Property(oo => oo.PhoneNumber)
            .HasColumnType("nvarchar(20)");

        builder.Property(oo => oo.CarSpecifications);

        builder.Property(oo => oo.NumberPlates)
            .HasColumnType("nvarchar(20)");

        builder.Property(oo => oo.AccountNumber)
            .HasColumnType("nvarchar(50)");

        builder.Property(oo => oo.BankId);

        builder.Property(oo => oo.CardNumber)
            .HasColumnType("nvarchar(30)");

        builder.Property(oo => oo.AccountName)
            .HasColumnType("nvarchar(250)");

        builder.Property(oo => oo.IBAN)
            .HasColumnType("nvarchar(50)");

        builder.Property(oo => oo.CertificateNumber)
            .HasColumnType("nvarchar(50)");

        builder.Property(oo => oo.Price)
            .HasColumnType("decimal(18, 2)");

        builder.Property(oo => oo.CurrencyUnitId);

        builder.Property(oo => oo.AccountDescription)
            .HasColumnType("nvarchar(1500)");

        builder.Property(oo => oo.TransportationRequestStatus)
            .HasDefaultValue(TransportationRequestStatus.InitialRegistration)
            .IsRequired();

        builder.Property(oo => oo.TransportationPaymentType);

        builder.Property(oo => oo.ManagerDescription)
            .HasColumnType("nvarchar(1500)");

        builder.Property(oo => oo.CarID)
            .HasColumnType("nvarchar(20)");

        builder.Property(oo => oo.ConfirmUserId);

        builder.Property(oo => oo.ConfirmDate);

        builder.Property(oo => oo.IsDeleted)
            .HasDefaultValue(false)
            .IsRequired();

        builder.Property(oo => oo.IsCredit)
            .HasDefaultValue(false)
            .IsRequired();

        builder.Property(oo => oo.IsAggregate)
            .HasDefaultValue(false)
            .IsRequired();

        builder.Property(oo => oo.SnapRequester);

        builder.Property(oo => oo.SecondDestinationCityId);

        builder.Property(oo => oo.FareAmount)
            .HasColumnType("decimal(18,2)");

        builder.Property(oo => oo.StopRate);

        builder.Property(oo => oo.DestinationAddress)
            .HasColumnType("nvarchar(1000)");

        builder.Property(oo => oo.SecondDestinationAddress)
            .HasColumnType("nvarchar(1000)");

        builder.Property(oo => oo.PersonalPayment)
            .HasDefaultValue(false);

        builder.Property(oo => oo.PassengerId);

        builder.Property(oo => oo.TicketPayerId);

        builder.Property(oo => oo.StartingCityAddress)
            .HasColumnType("nvarchar(1000)");

        builder.Property(oo => oo.ReturnToStart)
            .HasDefaultValue(false);

        builder.Property(oo => oo.Created)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(oo => oo.CostCategoryId);

        builder.Property(oo => oo.CostGroupId);

        builder.Property(oo => oo.CreatorId)
            .IsRequired();

        builder.HasOne(oo => oo.Transportation)
            .WithMany(oo => oo.TransportationRequests)
            .HasForeignKey("TransportationId").HasPrincipalKey(nameof(Transportation.Id))
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(oo => oo.Trip)
            .WithMany(oo => oo.TransportationRequests)
            .HasForeignKey("TripId").HasPrincipalKey(nameof(Trip.Id))
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(oo => oo.MachineType)
            .WithMany(oo => oo.TransportationRequests)
            .HasForeignKey("MachineTypeId").HasPrincipalKey(nameof(MachineType.Id))
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(oo => oo.Project)
            .WithMany(oo => oo.TransportationRequests)
            .HasForeignKey("ProjectId").HasPrincipalKey(nameof(Project.Id))
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(oo => oo.BillOfLading)
            .WithMany(oo => oo.TransportationRequests)
            .HasForeignKey("BillOfLadingId").HasPrincipalKey(nameof(BillOfLading.Id))
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(oo => oo.CostCenter)
            .WithMany(oo => oo.TransportationRequests)
            .HasForeignKey("CostCenterId").HasPrincipalKey(nameof(CostCenter.Id))
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(c => c.TransportationRequestDocuments)
            .WithOne(c => c.TransportationRequest)
            .HasForeignKey("TransportationRequestId")
            .HasPrincipalKey(nameof(TransportationRequest.Id))
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(oo => oo.Season)
            .WithMany(oo => oo.TransportationRequests)
            .HasForeignKey("SeasonId")
            .HasPrincipalKey(nameof(Season.Id))
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(oo => oo.TransportationContractor)
            .WithMany(oo => oo.TransportationRequests)
            .HasForeignKey(x => x.TransportationContractorId)
            .HasPrincipalKey(x => x.Id)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(oo => oo.TransportationRequestDetails)
            .WithOne(oo => oo.TransportationRequest)
            .HasForeignKey(oo => oo.TransportationRequestId)
            .HasPrincipalKey(oo => oo.Id)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(oo => oo.Driver)
            .WithMany()
            .HasForeignKey(x => x.DriverId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(oo => oo.StartingCity)
            .WithMany()
            .HasForeignKey(x => x.StartingCityId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(oo => oo.DestinationCity)
            .WithMany()
            .HasForeignKey(x => x.DestinationCityId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
