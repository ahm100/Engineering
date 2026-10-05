using Engineering.Application.Extensions;
using Engineering.Domain.Entities.Logistics;
using Engineering.Domain.Entities.MachineTypes;
using Engineering.Domain.Entities.Tasks;
using Engineering.Domain.Entities.Transportations;
using Gita.Backend.Shared.Application.Abstractions.Interfaces;
using Gita.Backend.Shared.Domain.Base;
using IdentityServer.ClientSdk.Services;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Linq.Expressions;
using System.Reflection;

namespace Engineering.Persistence;

public class EngineeringDBContext : DbContext
{
    private readonly IUserInfoService _userInfoService;
    private const string IsDeletedProperty = "IsDeleted";
    private const string CompanyIdProperty = "CompanyId";

    public EngineeringDBContext(
        DbContextOptions<EngineeringDBContext> options,
        IUserInfoProvider userInfoProvider) : base(options)
    {
        _userInfoService = userInfoProvider;
    }

    public long? CurrentCompanyId => _userInfoService.UserCompanyId;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("engineer");

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.HasSequence<long>("RequestMachinaryBill_BillNumber", "engineer");

        modelBuilder.HasSequence<long>("Contract_ContractNumber", "engineer");

        base.OnModelCreating(modelBuilder);

        modelBuilder.AddQueryFilterToAllEntitiesAssignableFrom<IAuditableEntity<long>>(x => !x.IsDeleted);
        modelBuilder.AddQueryFilterToAllEntitiesAssignableFrom<IActivateEntity<long>>(x => !x.IsDeleted);

        modelBuilder.Entity<TransportationRequest>().HasQueryFilter(o => o.CompanyId == CompanyValidator.GetCompanyId(_userInfoService) && !o.IsDeleted);
        modelBuilder.Entity<TransportationContractor>().HasQueryFilter(o => o.CompanyId == CompanyValidator.GetCompanyId(_userInfoService) && !o.IsDeleted);
        modelBuilder.Entity<TransportationContractorManager>().HasQueryFilter(o => !o.IsDeleted);
        modelBuilder.Entity<MachineType>().HasQueryFilter(o => o.CompanyId == CompanyValidator.GetCompanyId(_userInfoService) && !o.IsDeleted);
        modelBuilder.Entity<CabinType>().HasQueryFilter(o => o.CompanyId == CompanyValidator.GetCompanyId(_userInfoService) && !o.IsDeleted);
        modelBuilder.Entity<UserTask>()
            .HasOne(x => x.TaskGroup)
            .WithMany(x => x.UserTasks)
            .HasForeignKey(x => x.TaskGroupId)
            .OnDelete(DeleteBehavior.Restrict);


        //foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        //{
        //    if (entityType.IsOwned())
        //        continue;

        //    var clrType = entityType.ClrType;

        //    var hasIsDeleted = clrType.GetProperty(IsDeletedProperty) != null;
        //    var hasCompanyId = clrType.GetProperty(CompanyIdProperty) != null;

        //    if (hasIsDeleted || hasCompanyId)
        //        ApplyGlobalFilter(modelBuilder, entityType, hasIsDeleted, hasCompanyId);
        //}
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.LogTo(Console.WriteLine);
    }

    private void ApplyGlobalFilter(
        ModelBuilder modelBuilder,
        IMutableEntityType entityType,
        bool hasIsDeleted,
        bool hasCompanyId)
    {
        var parameter = Expression.Parameter(entityType.ClrType, "e");

        Expression? body = null;

        if (hasIsDeleted)
        {
            body = Expression.Equal(
                Expression.Property(parameter, IsDeletedProperty),
                Expression.Constant(false));
        }

        if (hasCompanyId)
        {
            var currentCompanyIdExpression = Expression.Property(
                Expression.Constant(this), nameof(CurrentCompanyId));

            var hasValueExpression = Expression.Property(
                currentCompanyIdExpression, nameof(Nullable<long>.HasValue));

            var currentValueExpression = Expression.Property(
                currentCompanyIdExpression, nameof(Nullable<long>.Value));

            Expression companyValueExpression = currentValueExpression;
            var companyProperty = Expression.Property(parameter, CompanyIdProperty);
            if (companyProperty.Type != currentValueExpression.Type)
                companyValueExpression = Expression.Convert(currentValueExpression, companyProperty.Type);

            var companyFilterExpression = Expression.OrElse(
                Expression.Not(hasValueExpression),
                Expression.Equal(companyProperty, companyValueExpression));

            body = body == null ? companyFilterExpression :
                Expression.AndAlso(body, companyFilterExpression);
        }

        if (body == null)
            return;

        var lambda = Expression.Lambda(body, parameter);
        modelBuilder.Entity(entityType.ClrType).HasQueryFilter(lambda);
    }

}
