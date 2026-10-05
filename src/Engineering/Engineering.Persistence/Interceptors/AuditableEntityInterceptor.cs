using Engineering.Application.Abstractions.Interfaces;
using Gita.Backend.Shared.Application.Abstractions.Interfaces;
using Gita.Backend.Shared.Domain.Base;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Engineering.Persistence.Interceptors;

public class AuditableEntityInterceptor : SaveChangesInterceptor
{
    private readonly IDateTime _dateTime;
    private readonly IUserProfileService _userProfileService;

    public AuditableEntityInterceptor(IDateTime dateTime, IUserProfileService userProfileService)
    {
        _dateTime = dateTime;
        _userProfileService = userProfileService;
    }

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        UpdateEntities(eventData.Context);

        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,
        InterceptionResult<int> result, CT ct = default)
    {
        UpdateEntities(eventData.Context);

        return base.SavingChangesAsync(eventData, result, ct);
    }

    public long? GetUserID()
    {
        var user = _userProfileService.GetProfileInfo();
        return user.UserId;
    }

    /// <summary>Audit را تکمیل می کند؛ تغییرات دارای CheckUser به پروفایل HTTP نیاز ندارند.</summary>
    private void UpdateEntities(DbContext? context)
    {
        if (context == null) return;
        var userId = new Lazy<long>(() => _userProfileService.GetProfileInfo().UserId);
        foreach (var entry in context.ChangeTracker.Entries<IAuditableEntity<long>>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.Created = _dateTime.Now;

                if (entry.Entity.CheckUser)
                    continue;
                else
                    entry.Entity.CreatorId = userId.Value;
            }

            if (entry.State == EntityState.Deleted || entry.State == EntityState.Modified)
            {
                entry.Entity.Updated = _dateTime.Now;

                if (entry.Entity.CheckUser)
                    continue;
                else
                    entry.Entity.UpdaterId = userId.Value;
            }
        }
    }
}

public static class Extensions
{
    public static bool HasChangedOwnedEntities(this EntityEntry entry) =>
        entry.References.Any(r =>
            r.TargetEntry != null &&
            r.TargetEntry.Metadata.IsOwned() &&
            (r.TargetEntry.State == EntityState.Added || r.TargetEntry.State == EntityState.Modified));
}
